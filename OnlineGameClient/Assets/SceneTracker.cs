using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{

    public int currentScene;

    public SpriteRenderer renderer;

    public void SetCurrentScene(int newScene)
    {

        currentScene = newScene;
        
        CheckVisible();

    }

    public void CheckVisible()
    {
        
        //Debug.Log($"Updated cursor Visibilities, Current scene is {SceneManager.GetActiveScene().buildIndex}");
        renderer.enabled = (currentScene == SceneManager.GetActiveScene().buildIndex);
        
    }
    
    public void CheckVisible(int currentScene)
    {
        
        //Debug.Log($"Updated cursor Visibilities, Current scene is {currentScene}");
        renderer.enabled = (currentScene == this.currentScene);
        
    }

}
