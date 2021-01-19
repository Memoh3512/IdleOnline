using System;
using System.Numerics;

namespace GameServer
{
    [Serializable]
    public class Mage : Player
    {

        public Mage(int _id, string _username, string _password) : base(_id, _username, _password)
        {
            
            
            
        }
        
        //convert basic Player object to specific Mage, choosing team
        public Mage(Player ply) : base(ply.id, ply.username, ply.password, ply.cursorPosition)
        {

        }
        
        public override bool IsMage () {return true;}
        
    }
}