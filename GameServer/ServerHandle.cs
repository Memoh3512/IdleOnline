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

            Console.WriteLine($"{Server.clients[fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully as player #{fromClient}.");
            if (fromClient != clientIdCheck)
            {

                Console.WriteLine($"Player \"{username}\" (ID: {fromClient}) has assumed the wrong client ID ({clientIdCheck})! If you see this, something went very very wrong.");

            }

            //send le joueur dans le jeu
            Server.clients[fromClient].SendIntoGame(username);

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
            if (IdleMage.BuyManaUpgrade(upgrade))
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
