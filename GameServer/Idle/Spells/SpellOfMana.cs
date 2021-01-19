using System;
using System.Buffers.Text;
using GameServer.Spells;

namespace GameServer.Idle.Spells
{
    [Serializable]
    public class SpellOfMana : Spell
    {
        private bool firstBuy = true;
        
        public SpellOfMana() : base("Spell of Mana", IdleNumber.Zero(), new IdleNumber(0.1f))
        {
        }

        public override void Buy()
        {
            base.Buy();

            Program.saveData.idleMage.ManaPerSecond += value;
            Console.WriteLine($"DECIMAL IS {Program.saveData.idleMage.ManaPerSecond.GetDecimal()}");

            if (firstBuy)
            {
                firstBuy = false;
                cost += new IdleNumber(0.1f);
            }
            else
            {
                cost *= 2;
            }

        }
        
    }
}