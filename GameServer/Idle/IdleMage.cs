using System;
using System.Collections.Generic;
using System.Text;
using GameServer.Idle.Spells;
using GameServer.Spells;

namespace GameServer
{

    [Serializable]
    public enum ManaUpgrades
    {
        
        ManaSpell = 1,
        
    }
    [Serializable]
    public class IdleMage
    {

        //mana resource
        public IdleNumber Mana = new IdleNumber(30);
        public IdleNumber ManaMax = new IdleNumber(100);
        public IdleNumber ManaPerSecond = IdleNumber.Zero();
        
        //mana Upgrade #1
        public SpellOfMana manaSpell = new SpellOfMana();

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
                
                case ManaUpgrades.ManaSpell:

                    if (CheckCost(manaSpell.GetCost()))
                    {
                        
                        manaSpell.Buy();
                        
                        ServerSend.PlayerBoughtSpell(upgradeType, manaSpell);
                        
                        return true;

                    }
                    break;
                
            }

            return false;

        }

        public bool CheckCost(IdleNumber cost)
        {

            return Mana >= cost;

        }

    }
}
