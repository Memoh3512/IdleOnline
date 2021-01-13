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

        SceneManager.LoadScene((int)nextScene);
        ClientSend.ChangeScene((int)nextScene);

    }

    public static void GoToErrorScreen(string errorMessage)
    {

        ErrorMessageText.currentErrorMessage = errorMessage;
        SceneManager.LoadScene("ErrorScreen");
        //Debug.Log("WENT TO ERROR SCREEN!!!!");

    }

}
