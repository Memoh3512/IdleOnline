using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{

    public int currentScene;

    public CanvasGroup canvasGroup;

    public void SetCurrentScene(int newScene)
    {

        currentScene = newScene;
        
        CheckVisible();

    }

    public void CheckVisible()
    {
        
        //Debug.Log($"Updated cursor Visibilities, Current scene is {SceneManager.GetActiveScene().buildIndex}");
        canvasGroup.alpha = (currentScene == SceneManager.GetActiveScene().buildIndex) ? 1 : 0;
        
    }
    
    public void CheckVisible(int currentScene)
    {
        
        //Debug.Log($"Updated cursor Visibilities, Current scene is {currentScene}");
        canvasGroup.alpha = (currentScene == this.currentScene) ? 1 : 0;
        
    }

}
