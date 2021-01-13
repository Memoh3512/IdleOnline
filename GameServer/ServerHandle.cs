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
            
            //TODO Check login and ENCRYPT
            string username = packet.ReadString();
            string password = packet.ReadString();

            Dictionary<string, string> users = Program.saveData.users;
            if (users.ContainsKey(username))
            {

                if (users[username] == password)
                {
                    
                    Server.clients[fromClient].LoginSuccessful(username,false);
                    
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
                    
                    //new user
                    Program.saveData.users.Add(username, password);
                    Server.clients[fromClient].LoginSuccessful(username,true);
                    
                }
                
            }

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
