using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//idk where to put this so I'll put it here
[Serializable]
public enum PlayerTypes
{
        
    Undefined = 1, Hunter = 2, Mage = 3
        
}
public class GameManager : MonoBehaviour
{
    
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    //public GameObject localPlayerCursorPrefab;
    public GameObject playerCursorPrefab;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="username"></param>
    /// <param name="spawnPos"></param>
    public void SpawnPlayer(int id, string username, int currentScene)
    {
        
        GameObject player;
        if (id == Client.GetMyId()) // si c'est le joueur local
        {
            player = GameObject.FindGameObjectWithTag("LocalPlayer");

        }
        else //si c'est un autre joueur
        {
            //Debug.Log("OTHER PLAYER JOINED!!!!!");
            player = Instantiate(playerCursorPrefab, Vector3.zero, Quaternion.identity);
            player.GetComponent<SceneTracker>().SetCurrentScene(currentScene);
        }

        //Assign values related to player
        player.GetComponent<PlayerManager>().id = id;
        player.GetComponent<PlayerManager>().username = username;
        players.Add(id, player.GetComponent<PlayerManager>());
    }
}