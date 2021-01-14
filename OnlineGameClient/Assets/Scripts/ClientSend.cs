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

            packet.Write(Client.GetMyId());
            packet.Write(OnlineConnectionManager.instance.usernameField.text);
            packet.Write(OnlineConnectionManager.instance.passwordField.text); //TODO Hash encrypt password and username
            
            SendTCPData(packet);

        }

    }

    public static void Login(string username, string password)
    {

        using (Packet packet = new Packet((int) ClientPackets.Login))
        {
            
         packet.Write(username);
         packet.Write(password);
         
         SendTCPData(packet);
            
        };

    }

    public static void ChangedScene(int newScene)
    {

        using (Packet packet = new Packet((int) ClientPackets.playerChangeScene))
        {
            
         packet.Write(newScene);
         
         SendTCPData(packet);
            
        };

    }

    public static void ManualSave()
    {

        using (Packet packet = new Packet((int) ClientPackets.ManualSave))
        {
            
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

    public static void BuyManaUpgrade(ManaUpgrades upgrade)
    {

        using (Packet packet = new Packet((int) ClientPackets.BuyManaUpgrade))
        {
            
            packet.Write((int)upgrade);

            SendTCPData(packet); //tcp paske faut pas le perdre ce packet la

        }
        
    }

    #endregion
}
