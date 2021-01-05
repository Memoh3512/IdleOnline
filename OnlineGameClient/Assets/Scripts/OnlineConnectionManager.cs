using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OnlineConnectionManager : MonoBehaviour
{

    public static OnlineConnectionManager instance;


    public GameObject connectionPanel;
    public TMP_InputField usernameField;
    public TMP_InputField ipField;
    public TMP_InputField portField;
    public Button btnConnect;
    
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

    public void ConnectToServer()
    {
        
        //btnConnect.interactable = false; TODO décommenter quand tout la connection network est setuppée


        Client.instance.ConnectToServer(ipField.text, Int32.Parse(portField.text));

    }

    /// <summary>
    /// sets the visibility of the connection menu
    /// </summary>
    /// <param name="active">Is the menu visible or not</param>
    public void SetConnectionMenu(bool active)
    {
        
        //Debug.Log("CLOSE MENU????");
        connectionPanel.SetActive(active);
        usernameField.interactable = active;

    }

    public void ResetBtn()
    {

        btnConnect.interactable = true;

    }

}
