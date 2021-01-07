using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void Start()
    {
        
        
        IdleNumber nb1 = new IdleNumber(1000000.00);
        IdleNumber nb2 = new IdleNumber(999999.99);

        IdleNumber.NumberDisplayType = NumberDisplayTypes.Full;
        
        Debug.Log($"nb1: {nb1.ToString()}\nb2: {nb2.ToString()}\nnb1 + nb2 = {(nb2-nb1).ToString()}");
        
    }

    public void Quit()
    {
        
        Application.Quit();
        
    }
    
}
