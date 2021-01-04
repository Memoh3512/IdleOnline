using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class ManaAmountText : MonoBehaviour
{
    private TextMeshProUGUI gui;
    //private float lastAmount = 0;
    //private float lastAmountPerSecond = 1;
    private void Awake()
    {
        gui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        gui.text = $"Mana:\t\t\t{IdleValues.Mana}\nper second:\t{IdleValues.ManaPerSecond}";

    }
}
