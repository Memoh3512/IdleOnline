using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneTypes
{
    
    TitleScreen = 0,        //TODO
    ConnectionScreen = 1,   //TODO
    OptionsScreen = 2,      //TODO
    MageScreen = 3,         //TODO
    HunterScreen = 4,       //TODO
    DisconnectScreen = 5,   //TODO
    
    
}

public static class SceneChanger
{

    public static void ChangeScene(SceneTypes nextScene)
    {

        SceneManager.LoadScene((int)nextScene);

    }

}
