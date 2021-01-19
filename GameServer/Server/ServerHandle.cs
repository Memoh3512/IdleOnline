using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class ServerHandle
    {

        public static void WelcomeReceived (int fromClient, Packet packet)
        {

            int clientIdCheck = packet.ReadInt();
            string username = packet.ReadString();
            string password = packet.ReadString();
            
            //Connection successful
            Console.WriteLine($"{Server.clients[fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully as player #{fromClient}.");
            
            //activer le login Screen
            Server.clients[fromClient].ToLoginScreen();

        }

        public static void Login(int fromClient, Packet packet)
        {
            
            string username = packet.ReadString();
            string password = packet.ReadString();

            Dictionary<string, Player> users = Program.saveData.users;
            if (users.ContainsKey(username))
            {

                if (users[username].password == password)
                {
                    
                    Server.clients[fromClient].LoginSuccessful(username);
                    
                }
                else
                {
                    
                    ServerSend.LoginFailed(fromClient, "Incorrect password!");
                    
                }
                
            }
            else
            {

                if (users.Count >= Server.MaxPlayers)
                {
                    
                    ServerSend.LoginFailed(fromClient, "Server full!!!! pls make this string more detailed");
                    
                }
                else
                {

                    Console.WriteLine($"New user Connected! Welcome, {username}");
                    //successful login
                    Server.clients[fromClient].LoginSuccessful(username, password);
                    
                }
                
            }

        }

        public static void PlayerChangedScene(int fromClient, Packet packet)
        {

            int newScene = packet.ReadInt();

            Server.clients[fromClient].player.SetCurrentScene(newScene);

        }

        public static void PlayerChoseTeam(int fromClient, Packet packet)
        {

            PlayerTypes team = (PlayerTypes) packet.ReadInt();

            switch (team)
            {
                
                case PlayerTypes.Hunter:
                    Server.clients[fromClient].player = new Hunter(Server.clients[fromClient].player);
                    break;
                
                case PlayerTypes.Mage:
                    Server.clients[fromClient].player = new Mage(Server.clients[fromClient].player);
                    break;

            }

            Server.clients[fromClient].player.team = team;
            
            //update users in save file, TODO might not be necessary to save, but might be nice
            Program.saveData.users[Server.clients[fromClient].player.username] = Server.clients[fromClient].player;
            //Program.saveData.SaveGameData();

            Console.WriteLine($"Assigning Client #{fromClient} the team {Enum.GetName(typeof(PlayerTypes),team)}. The player type is now {Server.clients[fromClient].player.GetType()}");
            
        }
        
        public static void ManualSave(int fromClient, Packet packet)
        {
            
            Program.saveData.SaveGameData();
            
        }

        public static void PlayerCursorMovement(int fromClient, Packet packet)
        {

            Vector3 newPosition = packet.ReadVector3();

            Server.clients[fromClient].player.SetCursorPosition(newPosition);

        }

        public static void BuyManaUpgrade(int fromClient, Packet packet)
        {

            ManaUpgrades upgrade = (ManaUpgrades)packet.ReadInt();
            //write message if upgrade is bought
            if (Program.saveData.idleMage.BuyManaUpgrade(upgrade))
            {
                
                Console.WriteLine($"Player #{fromClient} successfully bought Mana upgrade {Enum.GetName(typeof(ManaUpgrades),upgrade)}");
                
            }
            else
            {
                
                Console.WriteLine($"Player #{fromClient} tried to buy Mana upgrade {Enum.GetName(typeof(ManaUpgrades),upgrade)}, but doesnt't have enough mana!");
                
            }

        }

    }
}
