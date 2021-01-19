using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaUpgradeButton : MonoBehaviour
{

    public ManaUpgrades upgrade;

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void BuyUpgrade()
    {
        
        ClientSend.BuyManaUpgrade(upgrade);
        
    }

    private void Update()
    {
        
        UpdateDisplay();
        
    }

    private void UpdateDisplay()
    {
        
        text.text = $"Spell of Mana (+{IdleValues.ManaSpell.value}/s)\nCosts {IdleValues.ManaSpell.cost} Mana";
        //Debug.Log("SOME PLAYER BOUGHT MANA SPELL!!!!");

    }

}
