using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    class Server
    {

        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();

        public delegate void packetHandler(int fromClient, Packet packet);
        public static Dictionary<int, packetHandler> packetHandlers;

        private static TcpListener tcpListener;
        private static UdpClient udpListener;

        public static void Start (int _MaxPlayers, int _Port)
        {

            MaxPlayers = _MaxPlayers;
            Port = _Port;

            Console.WriteLine("Starting Server...");
            InitializeServerData();

            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            udpListener = new UdpClient(Port);
            udpListener.BeginReceive(UDPReceiveCallback, null);

            Console.WriteLine($"Server started on {Port}.");

        }

        private static void TCPConnectCallback (IAsyncResult _result)
        {

            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);
            Console.WriteLine($"Incoming connection from {_client.Client.RemoteEndPoint}");

            for (int i = 1; i <= MaxPlayers; i++)
            {

                if (clients[i].tcp.socket == null)
                {

                    clients[i].tcp.Connect(_client);
                    return;

                }

            }

            Console.WriteLine($"{_client.Client.RemoteEndPoint} failed to connect: Server full!");

        }

        private static void UDPReceiveCallback (IAsyncResult result)
        {

            try
            {

                IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpListener.EndReceive(result, ref clientEndPoint);
                udpListener.BeginReceive(UDPReceiveCallback, null);

                if (data.Length < 4)
                {

                    return;

                }

                using (Packet packet = new Packet(data))
                {

                    int clientId = packet.ReadInt();

                    if (clientId == 0)
                    {

                        return;

                    }

                    if (clients[clientId].udp.endPoint == null)
                    {

                        clients[clientId].udp.Connect(clientEndPoint);
                        return;

                    }

                    if (clients[clientId].udp.endPoint.ToString() == clientEndPoint.ToString())
                    {

                        clients[clientId].udp.HandleData(packet);

                    }

                }

            } catch(Exception ex)
            {

                Console.WriteLine($"Error receiving UDP data: {ex}");

            }

        }

        public static void SendUDPData(IPEndPoint clientEndPoint, Packet packet)
        {

            try 
            {
            
                if (clientEndPoint != null)
                {

                    udpListener.BeginSend(packet.ToArray(), packet.Length(), clientEndPoint, null, null);

                }
            
            } catch (Exception ex)
            {

                Console.WriteLine($"Error sending data to {clientEndPoint} via UDP: {ex}");

            }

        }

        private static void InitializeServerData ()
        {

            for (int i = 1; i <= MaxPlayers; i++)
            {

                clients.Add(i, new Client(i));

            }
            packetHandlers = new Dictionary<int, packetHandler>()
            {

                {(int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
                {(int)ClientPackets.playerMovement, ServerHandle.PlayerCursorMovement },
                {(int)ClientPackets.BuyManaUpgrade, ServerHandle.BuyManaUpgrade},
                {(int)ClientPackets.Login, ServerHandle.Login},
                {(int)ClientPackets.ManualSave, ServerHandle.ManualSave},
                {(int)ClientPackets.playerChangeScene, ServerHandle.PlayerChangedScene}

            };
            Console.WriteLine("Initialized packets.");

        }

        public static int[] GetNotLoggedPlayers(bool addSelf = false, int selfId = 1)
        {

            List<int> res = new List<int>();
            foreach (Client cl in clients.Values)
            {
                
                if (!cl.isLoggedIn) res.Add(cl.id);
                
            }

            if (addSelf)
            {
                if (!res.Contains(selfId)) res.Add(selfId);
            }

            return res.ToArray();

        }

    }
}
