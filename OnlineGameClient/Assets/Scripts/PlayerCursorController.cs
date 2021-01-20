using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursorController : MonoBehaviour
{
    private bool visible = false;
    private bool following = true;

    public GameObject basicCursorSprite;
    public GameObject hunterCursorSprite;
    public GameObject mageCursorSprite;


    private void Start()
    {
        Cursor.visible = visible;
        SetCursorSprite(PlayerTypes.Undefined);
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

    public void SetCursorSprite(PlayerTypes team)
    {

        //clears children
        for (int i = 0; i < transform.childCount; i++)
        {
            
            Destroy(transform.GetChild(i).gameObject);
            
        }

        GameObject newSprite;
        switch (team)
        {
            
            case PlayerTypes.Mage:
                newSprite = Instantiate(mageCursorSprite, transform);
                newSprite.transform.localPosition = Vector3.zero;
                Debug.Log("CURSOR CHANGE MAGEEE!!!");
                
                break;
            case PlayerTypes.Hunter:

                //TODO make hunter cursor
                //newSprite = Instantiate(mageCursorSprite, transform);
                //newSprite.transform.localPosition = Vector3.zero;
                
                break;
            
            default:

                newSprite = Instantiate(basicCursorSprite, transform);
                newSprite.transform.localPosition = Vector3.zero;
                break;
            
        }


        /*TODO other players cursor sprites change
        if (!CompareTag("LocalPlayer"))
        {
            
            newSprite.GetComponent<SpriteRenderer>().color = Color.cyan;

        }*/
        
    }

}
