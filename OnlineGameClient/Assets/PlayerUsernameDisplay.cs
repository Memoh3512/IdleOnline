using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUsernameDisplay : MonoBehaviour
{


    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {

        GetComponent<TextMeshProUGUI>().text = playerManager.username;

    }
    
}
