using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[Serializable]
public enum ManaUpgrades
{
        
    ManaSpell = 1,
        
}

public class IdleValues : MonoBehaviour
{

    //manaValues (Stored in Server, this is used for display)
    public static string Mana = "";
    public static string MaxMana = "";
    public static string ManaPerSecond = "";

    public static readonly Spell ManaSpell = new Spell();

    public static void UpdateSpellDisplay(ManaUpgrades type, string cost, string value)
    {

        switch (type)
        {
            
            case ManaUpgrades.ManaSpell:

                ManaSpell.cost = cost;
                ManaSpell.value = value;
                
                break;
            
        }
        
    }


}
