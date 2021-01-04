using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet packet)
    {
        
        packet.WriteLength();
        Client.instance.tcp.SendData(packet);

    }

    private static void SendUDPData(Packet packet)
    {
        
        packet.WriteLength();
        Client.instance.udp.SendData(packet);
        
    }

    #region Packets

    public static void WelcomeReceived()
    {
        
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {

            packet.Write(Client.instance.myID);
            packet.Write(OnlineConnectionManager.instance.usernameField.text);
            
            SendTCPData(packet);

        }

    }

    public static void SendCursorPosToServer(Vector3 position)
    {
        

        using (Packet packet = new Packet((int) ClientPackets.playerMovement))
        {
         
            packet.Write(position);

            SendUDPData(packet);

        };
        
    }

    #endregion
}
