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
        public static double Mana = 30; //TODO this is an IdleNumber
        public static double ManaMax = 100; //TODO IdleNumber
        public static double ManaPerSecond; //TODO IdleNumber
        
        //mana Upgrade #1
        static float U1PerSecBoost = 1f;//TODO IdleNumber
        static float U1Cost = 10f;//TODO IdleNumber

        public static void Update()
        {
            
            Mana = Math.Min(Mana + (ManaPerSecond / Constants.TICKS_PER_SEC),ManaMax);
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

        public static bool CheckCost(float Cost)
        {

            return Mana >= Cost;

        }

        public static void BuyU1()
        {

            Mana -= U1Cost;//TODO soustraction IdleNumber
            ManaPerSecond += U1PerSecBoost;

        }


    }
}
