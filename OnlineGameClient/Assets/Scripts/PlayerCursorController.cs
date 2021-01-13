using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursorController : MonoBehaviour
{
    private bool visible = false;
    private bool following = true;


    private void Start()
    {
        Cursor.visible = visible;
    }

    private void Update()
    {

        Camera cam = Camera.main;
        
        if (cam != null && following) // cursor follow mouse
        {
            
            Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldPos.x, worldPos.y, 0);

        }

        if (Input.GetKeyDown(KeyCode.T)) // set cursor visible and following through key
        {

            Cursor.visible = visible = !visible;
            following = !following;

        }
        
    }

    private void FixedUpdate()
    {

        if (Client.instance != null)
        {
            
            if (Client.instance.isLoggedIn)
            {
            
                SendCursorPosToServer();   
            
            }   
            
        }

    }

    private void SendCursorPosToServer()
    {

        Vector3 cursorPos = transform.position;

        ClientSend.SendCursorPosToServer(cursorPos);

    }
    
}
