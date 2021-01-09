using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GameServer
{
    class ServerSend
    {

        private static void SendTCPData(int toClient, Packet packet)
        {

            packet.WriteLength();
            Server.clients[toClient].tcp.SendData(packet);

        }

        private static void SendUDPData(int toClient, Packet packet)
        {

            packet.WriteLength();
            Server.clients[toClient].udp.SendData(packet);

        }

        private static void SendTCPDataToAll (Packet packet)
        {

            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {

                Server.clients[i].tcp.SendData(packet);

            }

        }

        private static void SendTCPDataToAll(Packet packet, int[] exceptClients)
        {

            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (!exceptClients.Contains(i))
                {

                    Server.clients[i].tcp.SendData(packet);

                }

            }

        }

        private static void SendUDPDataToAll(Packet packet)
        {

            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {

                Server.clients[i].udp.SendData(packet);

            }

        }

        private static void SendUDPDataToAll(Packet packet, int[] exceptClients)
        {

            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (!exceptClients.Contains(i))
                {

                    Server.clients[i].udp.SendData(packet);

                }

            }

        }

        #region Packets

        public static void Welcome(int toClient, string msg)
        {

            using (Packet packet = new Packet((int)ServerPackets.welcome))
            {

                packet.Write(msg);
                packet.Write(toClient);

                SendTCPData(toClient, packet);

            }

        }

        public static void SpawnPlayer(int toClient, Player player)
        {

            using (Packet packet = new Packet((int)ServerPackets.spawnPlayer))
            {

                packet.Write(player.id);
                packet.Write(player.username);
                packet.Write(player.cursorPosition);

                SendTCPData(toClient, packet);

            };

        }
        public static void PlayerCursorPosition(Player player)
        {

            using (Packet packet = new Packet((int)ServerPackets.playerCursorPosition))
            {

                packet.Write(player.id);
                packet.Write(player.cursorPosition);

                SendUDPDataToAll(packet, new int[] {player.id});

            };

        }

        public static void PlayerDisconnected (int playerID)
        {

            using (Packet packet = new Packet((int)ServerPackets.playerDisconnected))
            {

                packet.Write(playerID);

                SendTCPDataToAll(packet);

            };

        }

        public static void UpdateIdle(IdleNumber mana, IdleNumber manaPerSec, IdleNumber maxMana)
        {

            using (Packet packet = new Packet((int)ServerPackets.IdleUpdate))
            {

                packet.Write(mana.ToString());
                packet.Write(manaPerSec.ToString());
                packet.Write(maxMana.ToString());
                
                SendUDPDataToAll(packet);

            };

        }

        #endregion

    }
}
