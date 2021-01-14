﻿using System;
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

    public static void LoginSuccessful(Packet packet)
    {

        bool firstTime = packet.ReadBool();
        
        //go to proper scene if its first time or not
        if (firstTime)
        {
            
            //Debug.Log("CHANGING SCENE!!!");
            SceneChanger.ChangeScene(SceneTypes.TeamSelectionScreen);
                
        }
        else
        {
                
            SceneChanger.ChangeScene(SceneTypes.MageScreen);
                
        }
        
    }

    public static void SpawnPlayer(Packet packet)
    {

        int id = packet.ReadInt();
        string username = packet.ReadString();
        int currentScene = packet.ReadInt();

        Client.instance.isLoggedIn = true;
        
        GameManager.instance.SpawnPlayer(id, username, currentScene);

    }

    public static void PlayerChangedScene(Packet packet)
    {

        int id = packet.ReadInt();
        int newScene = packet.ReadInt();

        GameManager.players[id].GetComponent<SceneTracker>().SetCurrentScene(newScene);

    }

    public static void PlayerCursorPosition(Packet packet)
    {
        
        int id = packet.ReadInt();
        Vector3 newCursorPos = packet.ReadVector3();

        //if to fix crash when we are connected but not logging in so we are receiving all cmds 
        if (GameManager.players.ContainsKey(id)) 
        {
            
            GameManager.players[id].GetComponent<PosInterpolation>().AddMovement(newCursorPos);
            
        }

        //GameManager.players[id].transform.position = newCursorPos; //Old method, very laggy

    }

    public static void UpdateMana(Packet packet)
    {

        string amount = packet.ReadString();
        string amountPerSec = packet.ReadString();
        string max = packet.ReadString();
        
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

    public static void ToLoginScreen(Packet packet)
    {
        
        OnlineConnectionManager.instance.SwitchMenu();
        
    }

    public static void LoginFailed(Packet packet)
    {

        string msg = packet.ReadString();
        Debug.Log(msg);

    }

}
