using System;

namespace GameServer.Spells
{
    [Serializable]
    public class Spell
    {

        private string name;
        protected IdleNumber cost;
        protected IdleNumber value; //upgrade number. can upgrade anything so this number is arbitrary
        
        public Spell(string _name, IdleNumber _cost, IdleNumber upgradeVal)
        {

            name = _name;
            cost = _cost;
            value = upgradeVal;
            

        }
        
        public virtual void Buy()
        {

            Program.saveData.idleMage.Mana -= cost;
            

        }

        public IdleNumber GetCost()
        {

            return cost;

        }

        public IdleNumber GetValue()
        {

            return value;

        }

    }
}