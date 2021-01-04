using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Numerics;

namespace GameServer
{
    class Client
    {
        public static int dataBufferSize = 4096;
        public int id;
        public Player player;
        public TCP tcp;
        public UDP udp;

        public Client (int _clientId)
        {

            id = _clientId;
            tcp = new TCP(id);
            udp = new UDP(id);

        }

        public class TCP
        {

            public TcpClient socket;

            private readonly int id;
            private NetworkStream stream;
            private Packet receivedData;
            private byte[] receiveBuffer;

            public TCP(int _id)
            {

                id = _id;

            }

            public void Connect(TcpClient _socket)
            {

                socket = _socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                receivedData = new Packet();
                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

                ServerSend.Welcome(id, "Welcome to the server!");

            }

            public void SendData (Packet packet) 
            {

                try
                {

                    if (socket != null)
                    {

                        stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);

                    }

                } catch (Exception ex)
                {

                    Console.WriteLine($"Error sending data to player {id} via TCP: {ex}");

                }

            }

            private void ReceiveCallback(IAsyncResult _result)
            {

                try
                {

                    int byteLength = stream.EndRead(_result);
                    if (byteLength <= 0) {

                        Server.clients[id].Disconnect();
                        return;

                    }

                    byte[] data = new byte[byteLength];
                    Array.Copy(receiveBuffer, data, byteLength);

                    receivedData.Reset(HandleData(data));
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

                } catch(Exception ex)
                {

                    Console.WriteLine($"Error receiving TCP data: {ex}");
                    Server.clients[id].Disconnect();

                }

            }

            private bool HandleData(byte[] data)
            {

                int packetLength = 0;

                receivedData.SetBytes(data);

                if (receivedData.UnreadLength() >= 4)
                {

                    packetLength = receivedData.ReadInt();
                    if (packetLength < 1)
                    {

                        return true;

                    }

                }

                while (packetLength > 0 && packetLength <= receivedData.UnreadLength())
                {

                    byte[] packetBytes = receivedData.ReadBytes(packetLength);
                    ThreadManager.ExecuteOnMainThread(() =>
                    {

                        using (Packet packet = new Packet(packetBytes))
                        {

                            int packetId = packet.ReadInt();
                            Server.packetHandlers[packetId](id, packet);

                        }

                    });

                    packetLength = 0;

                    if (receivedData.UnreadLength() >= 4)
                    {

                        packetLength = receivedData.ReadInt();
                        if (packetLength < 1)
                        {

                            return true;

                        }

                    }
                }

                if (packetLength <= 1) return true;

                return false;

            }

            public void Disconnect()
            {

                socket.Close();
                stream = null;
                receivedData = null;
                receiveBuffer = null;
                socket = null;

            }

        }

        public class UDP
        {

            public IPEndPoint endPoint;

            private int id;

            public UDP(int  _id)
            {

                id = _id;

            }

            public void Connect(IPEndPoint _endPoint)
            {

                endPoint = _endPoint;

            }

            public void SendData(Packet packet)
            {

                Server.SendUDPData(endPoint, packet);

            }

            public void HandleData(Packet packetData)
            {

                int packetLength = packetData.ReadInt();
                byte[] packetBytes = packetData.ReadBytes(packetLength);

                ThreadManager.ExecuteOnMainThread(() =>
                {

                    using (Packet packet = new Packet(packetBytes))
                    {

                        int packetId = packet.ReadInt();
                        Server.packetHandlers[packetId](id, packet);

                    };

                });

            }

            public void Disconnect()
            {

                endPoint = null;

            }

        }

        public void SendIntoGame(string playerName)
        {

            player = new Player(id, playerName, new Vector3(0, 0, 0));

            foreach (Client client in Server.clients.Values)
            {

                if (client.player != null) // si le joueur est connecté
                {

                    if (client.id != id)
                    {

                        ServerSend.SpawnPlayer(id, client.player);

                    }

                }

            }

            foreach (Client client in Server.clients.Values)
            {

                if (client.player != null)
                {

                    ServerSend.SpawnPlayer(client.id, player);

                }

            }

        }

        private void Disconnect()
        {

            Console.WriteLine($"{tcp.socket.Client.RemoteEndPoint} has disconnected.");

            player = null;

            tcp.Disconnect();
            udp.Disconnect();

            ServerSend.PlayerDisconnected(id);

        }
    }
}
