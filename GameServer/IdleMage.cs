using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{

    [Serializable]
    public enum ManaUpgrades
    {
        
        Upgrade1 = 1,
        
    }
    [Serializable]
    public class IdleMage
    {

        //mana resource
        public IdleNumber Mana = new IdleNumber(30);
        public IdleNumber ManaMax = new IdleNumber(100);
        public IdleNumber ManaPerSecond = IdleNumber.Zero();
        
        //mana Upgrade #1
        IdleNumber U1PerSecBoost = new IdleNumber(1);
        private IdleNumber U1Cost = new IdleNumber(10);

        public void Update()
        {
            
            Mana = IdleMath.Min(Mana + (ManaPerSecond / Constants.TICKS_PER_SEC),ManaMax);
            ServerSend.UpdateIdle(Mana, ManaPerSecond, ManaMax);
            //Console.Write("Idle updated");

        }

        public bool BuyManaUpgrade(ManaUpgrades upgradeType)
        {

            switch (upgradeType)
            {
                
                case ManaUpgrades.Upgrade1:

                    if (CheckCost(U1Cost))
                    {
                        
                        BuyU1();
                        return true;

                    }
                    
                    break;
                
            }

            return false;

        }

        public bool CheckCost(IdleNumber Cost)
        {

            return Mana >= Cost;

        }

        public void BuyU1()
        {

            Mana -= U1Cost;
            ManaPerSecond += U1PerSecBoost;

        }


    }
}
