using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void Start()
    {
        
        
        IdleNumber nb1 = new IdleNumber(320000000);
        IdleNumber nb2 = new IdleNumber(999999999);

        IdleNumber.NumberDisplayType = NumberDisplayTypes.Named;
        
        Debug.Log($"nb1: {nb1.ToString()}\nb2: {nb2.ToString()}\nnb1 + nb2 = {(nb1+nb2).ToString()}");
        
    }

    public void Quit()
    {
        
        Application.Quit();
        
    }
    
}
