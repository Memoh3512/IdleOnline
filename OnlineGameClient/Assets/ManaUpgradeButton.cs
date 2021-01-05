using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUpgradeButton : MonoBehaviour
{

    public ManaUpgrades Upgrade = ManaUpgrades.Upgrade1;


    public void BuyUpgrade()
    {
        
        ClientSend.BuyManaUpgrade(Upgrade);
        
    }

}
