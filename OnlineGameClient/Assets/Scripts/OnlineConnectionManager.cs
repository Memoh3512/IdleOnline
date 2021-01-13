using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OnlineConnectionManager : MonoBehaviour
{

    public static OnlineConnectionManager instance;

    

    public TMP_InputField ipField;
    public TMP_InputField portField;
    
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;

    public GameObject ConnectionMenu;
    public GameObject LogInMenu;

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

    public void Login()
    {

        Debug.Log($"Encryption\nUsername : {usernameField.text}\nPassword: {Encrypter.EncryptData(passwordField.text)}");
        ClientSend.Login(usernameField.text, Encrypter.EncryptData(passwordField.text));

    }

    public void ConnectToServer()
    {
        
        //btnConnect.interactable = false; TODO décommenter quand tout la connection network est setuppée


        Client.instance.ConnectToServer(ipField.text, Int32.Parse(portField.text));

    }

    public void SwitchMenu()
    {
        
        ConnectionMenu.SetActive(false);
        LogInMenu.SetActive(true);
        
    }

}
