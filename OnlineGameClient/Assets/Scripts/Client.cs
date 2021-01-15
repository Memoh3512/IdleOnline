using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Client : MonoBehaviour
{

    public static Client instance;
    public static int dataBufferSize = 4096;
    
    private string ip = "127.0.0.1";
    private int port = 25565;
    private int myID = 0;
    public TCP tcp;
    public UDP udp;

    public bool isConnected = false;
    public bool isLoggedIn = false;
    private delegate void PacketHandler(Packet packet);

    private static Dictionary<int, PacketHandler> packetHandlers;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;

        } else if (instance != this)
        {
            
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
            
        }
    }

    private void OnApplicationQuit()
    {
        Disconnect();
    }

    public void SetIPAndPort(string _ip, int _port)
    {

        this.ip = _ip;
        this.port = _port;

    }

    public void ConnectToServer(string _ip, int _port)
    {
        
        SetIPAndPort(_ip, _port);
        
        tcp = new TCP();
        udp = new UDP();

        InitializeClientData();
        
        isConnected = true;

        tcp.Connect();

    }

    public static int GetMyId()
    {

        return instance.myID;

    }

    public static void SetId(int newId)
    {

        instance.myID = newId;

    }

    public class TCP
    {

        public TcpClient socket;

        private NetworkStream stream;
        private Packet receivedData;
        private byte[] receiveBuffer;
        public void Connect()
        {

            socket = new TcpClient
            {
                ReceiveBufferSize =  dataBufferSize,
                SendBufferSize = dataBufferSize

            };
            
            receiveBuffer = new byte[dataBufferSize];

            socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);

        }

        private void ConnectCallback(IAsyncResult result)
        {
            
            socket.EndConnect(result);

            if (!socket.Connected)
            {
                
                return;

            }

            stream = socket.GetStream();

            receivedData = new Packet();
            
            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

        }

        public void SendData(Packet packet)
        {

            try
            {

                stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);

            }
            catch (Exception ex)
            {
                
                Debug.Log($"Error sending data to server via TCP: {ex.Message}");
                
            }
            
        }
        
        private void ReceiveCallback(IAsyncResult result)
        {

            try
            {

                int byteLength = stream.EndRead(result);
                if (byteLength <= 0) {

                    instance.Disconnect(new Exception("Bytelength <= 0 in TCP's receiveCallback"));
                    return;

                }

                byte[] data = new byte[byteLength];
                Array.Copy(receiveBuffer, data, byteLength);

                receivedData.Reset(HandleData(data));
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

            } catch(Exception ex)
            {

                //Debug.Log($"Error receiving TCP data: {ex}");
                Disconnect(new Exception($"Error receiving TCP data: {ex.Message}"));

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
                        packetHandlers[packetId](packet);

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

        private void Disconnect(Exception ex)
        {
            
            instance.Disconnect(ex);
            
            stream = null;
            receivedData = null;
            receiveBuffer = null;
            socket = null;

        }
        
    }

    public class UDP
    {

        public UdpClient socket;
        public IPEndPoint endPoint;

        public UDP()
        {
            endPoint = new IPEndPoint(IPAddress.Parse(instance.ip), instance.port);
        }

        public void Connect(int localPort)
        {
            
            socket = new UdpClient(localPort);
            
            socket.Connect(endPoint);
            socket.BeginReceive(ReceiveCallback, null);

            using (Packet packet = new Packet())
            {

                SendData(packet);

            }

        }

        public void SendData(Packet packet)
        {

            try
            {

                packet.InsertInt(instance.myID);
                socket?.BeginSend(packet.ToArray(), packet.Length(), null, null);

            }
            catch (Exception ex)
            {
                
                Debug.Log($"Error sending data to server via UDP: {ex.Message}");
                
            }
            
        }

        private void ReceiveCallback(IAsyncResult result)
        {

            try
            {
                
                byte[] data = socket.EndReceive(result, ref endPoint);
                socket.BeginReceive(ReceiveCallback, null);

                if (data.Length < 4)
                {

                    instance.Disconnect(new Exception("data length < 4 in UDP's receiveCallback"));
                    return;

                }

                Handledata(data);

            }
            catch (Exception ex)
            {
                
                Disconnect(ex);
                
            }
            
        }

        private void Handledata(byte[] data)
        {

            using (Packet packet = new Packet(data))
            {

                int packetLength = packet.ReadInt();
                data = packet.ReadBytes(packetLength);

            }
            
            ThreadManager.ExecuteOnMainThread(() =>
            {

                using (Packet packet = new Packet(data))
                {

                    int packetId = packet.ReadInt();
                    packetHandlers[packetId](packet);

                }
                
            });
            
        }

        private void Disconnect(Exception ex)
        {
            
            instance.Disconnect(ex);

            endPoint = null;
            socket = null;

        }

    }

    private void InitializeClientData()
    {
        
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            {(int)ServerPackets.welcome, ClientHandle.Welcome},
            {(int)ServerPackets.playerCursorPosition, ClientHandle.PlayerCursorPosition},
            {(int)ServerPackets.playerDisconnected, ClientHandle.PlayerDisconnected},
            {(int)ServerPackets.IdleUpdate,ClientHandle.UpdateMana},
            {(int)ServerPackets.LogInFailed,ClientHandle.LoginFailed},
            {(int)ServerPackets.ToLoginScreen,ClientHandle.ToLoginScreen},
            {(int)ServerPackets.LoginSuccessful, ClientHandle.LoginSuccessful},//TODO CHange spawnPlayer to change scene and login and stuff
            {(int)ServerPackets.playerChangeScene, ClientHandle.PlayerChangedScene},
            {(int)ServerPackets.SpawnPlayer, ClientHandle.SpawnPlayer}
        };
        Debug.Log("Initialized Packets.");

    }
    
    private void Disconnect()
    {
        if (isConnected)
        {

            isConnected = false;
            tcp?.socket?.Close();
            udp?.socket?.Close();
            
            Debug.Log("Disconnected from server.");

        }
    }
    
    private void Disconnect(Exception ex)
    {
        if (isConnected)
        {

            isConnected = false;
            tcp?.socket?.Close();
            udp?.socket?.Close();
            
            //disconnected, go to disconnect screen
            SceneChanger.GoToErrorScreen($"Disconnected from server\n {ex.Message}");
            Debug.Log("Disconnected from server.");

        }
    }
    
}
