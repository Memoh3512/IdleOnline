using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NextScene))]
public class TeamChooseBtn : MonoBehaviour
{

    public PlayerTypes team;
    
    private NextScene nextScene;
    // Start is called before the first frame update
    void Start()
    {

        nextScene = GetComponent<NextScene>();

    }

    public void ChooseTeam()
    {

        //send to server the info that we chose a team
        ClientSend.PlayerChoseTeam(team);
        
        nextScene.ChangeScene();
        
    }
    
}
