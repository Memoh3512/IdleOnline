using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{

    public enum ManaUpgrades
    {
        
        Upgrade1 = 1,
        
    }
    
    class IdleMage
    {

        //mana resource
        public static IdleNumber Mana = new IdleNumber(30);
        public static IdleNumber ManaMax = new IdleNumber(100);
        public static IdleNumber ManaPerSecond = IdleNumber.Zero();
        
        //mana Upgrade #1
        static IdleNumber U1PerSecBoost = new IdleNumber(1);
        private static IdleNumber U1Cost = new IdleNumber(10);

        public static void Update()
        {
            
            Mana = IdleMath.Min(Mana + (ManaPerSecond / Constants.TICKS_PER_SEC),ManaMax);
            ServerSend.UpdateIdle(Mana, ManaPerSecond, ManaMax);
            //Console.Write("Idle updated");

        }

        public static bool BuyManaUpgrade(ManaUpgrades upgradeType)
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

        public static bool CheckCost(IdleNumber Cost)
        {

            return Mana >= Cost;

        }

        public static void BuyU1()
        {

            Mana -= U1Cost;
            ManaPerSecond += U1PerSecBoost;

        }


    }
}
