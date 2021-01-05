using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {

        string msg = packet.ReadString();
        int myId = packet.ReadInt();
        
        Debug.Log($"Message from server: {msg}");
        Client.instance.myID = myId;
        ClientSend.WelcomeReceived();
        
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);

    }

    public static void SpawnPlayer(Packet packet)
    {

        int id = packet.ReadInt();
        string username = packet.ReadString();
        Vector3 position = packet.ReadVector3();
        
        GameManager.instance.SpawnPlayer(id, username, position);

    }

    public static void PlayerCursorPosition(Packet packet)
    {
        
        int id = packet.ReadInt();
        Vector3 newCursorPos = packet.ReadVector3();

        GameManager.players[id].GetComponent<PosInterpolation>().StartInterpolating(newCursorPos);
        //GameManager.players[id].transform.position = newCursorPos; //Old method, very laggy

    }

    public static void UpdateMana(Packet packet)
    {

        double amount = packet.ReadDouble();
        double amountPerSec = packet.ReadDouble();
        double max = packet.ReadDouble();
        
        IdleValues.Mana = amount;
        IdleValues.ManaPerSecond = amountPerSec;
        IdleValues.MaxMana = max;

    }
    public static void PlayerDisconnected(Packet packet)
    {

        int id = packet.ReadInt();
        Destroy(GameManager.players[id].gameObject);
        GameManager.players.Remove(id);

    }

}
