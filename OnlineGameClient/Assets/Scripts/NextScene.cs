using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{

    public SceneTypes scene;
    
    public void ChangeScene()
    {
        
        SceneChanger.ChangeScene(scene);
        
    }
    
}
