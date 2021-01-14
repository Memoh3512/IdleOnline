using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneTypes
{
    
    TitleScreen = 0,        //TODO
    ConnectionScreen = 1,   //TODO
    MageScreen = 2,         //TODO
    HunterScreen = 3,       //TODO
    OptionsScreen = 4,      //TODO
    ErrorScreen = 5,        //TODO
    TeamSelectionScreen = 6,//TODO
    
    
}

public static class SceneChanger
{

    public static void ChangeScene(SceneTypes nextScene)
    {

        //if connected, tell server we changed scene
        if (Client.instance != null)
        {

            if (Client.instance.isLoggedIn)
            {

                ClientSend.ChangedScene((int) nextScene);

            }
            
        }
        
        //Change scene
        SceneManager.LoadScene((int)nextScene);
        
        //Update visibility of other players' cursors
        foreach (var player in GameManager.players.Values)
        {
            
            if (player.id != Client.GetMyId()) player.GetComponent<SceneTracker>().CheckVisible((int)nextScene);
            
        }

    }

    public static void GoToErrorScreen(string errorMessage)
    {

        ErrorMessageText.currentErrorMessage = errorMessage;
        SceneManager.LoadScene("ErrorScreen");
        //Debug.Log("WENT TO ERROR SCREEN!!!!");

    }

}
