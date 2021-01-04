using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    //public GameObject localPlayerCursorPrefab;
    public GameObject playerCursorPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SpawnPlayer(int id, string username, Vector3 spawnPos)
    {
        //Debug.Log($"TRYING TO SPAWN PLAYER #{id}");
        GameObject player;
        if (id == Client.instance.myID) // si c'est le joueur local
        {
            player = GameObject.FindGameObjectWithTag("LocalPlayer");
        }
        else //si c'est un autre joueur
        {
            player = Instantiate(playerCursorPrefab, spawnPos, Quaternion.identity);
        }

        player.GetComponent<PlayerManager>().id = id;
        player.GetComponent<PlayerManager>().username = username;
        players.Add(id, player.GetComponent<PlayerManager>());
    }
}