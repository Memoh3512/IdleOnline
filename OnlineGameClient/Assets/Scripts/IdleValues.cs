using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ManaUpgrades
{
        
    Upgrade1 = 1,
        
}

public class IdleValues : MonoBehaviour
{

    //manaValues
    public static IdleNumber Mana = new IdleNumber(30);
    public static IdleNumber MaxMana = new IdleNumber(100);
    public static IdleNumber ManaPerSecond = IdleNumber.Zero();
    

}
