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
    DisconnectScreen = 5,   //TODO
    
    
}

public static class SceneChanger
{

    public static void ChangeScene(SceneTypes nextScene)
    {

        SceneManager.LoadScene((int)nextScene);

    }

}
