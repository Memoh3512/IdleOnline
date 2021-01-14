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

        renderer.enabled = (currentScene == SceneManager.GetActiveScene().buildIndex);

    }

}
