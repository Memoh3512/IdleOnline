using System.Numerics;

namespace GameServer
{
    public class Mage : Player
    {

        public Mage(int _id, string _username, string _password) : base(_id, _username, _password)
        {
            
            
            
        }
        
        //convert basic Player object to specific Mage, choosing team
        public Mage(Player ply) : base(ply.id, ply.username, ply.password, ply.cursorPosition)
        {

            ply = null;

        }
        
        public override bool IsMage () {return true;}
        
    }
}