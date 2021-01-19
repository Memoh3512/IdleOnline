using System;
using System.Numerics;

namespace GameServer
{
    [Serializable]
    public class Hunter : Player
    {

        public Hunter(int _id, string _username, string _password) : base(_id, _username, _password)
        {
            
            
            
        }
        
        //convert basic Player object to specific Hunter, choosing team
        public Hunter(Player ply) : base(ply.id, ply.username, ply.password, ply.cursorPosition)
        {

        }

        public override bool IsHunter () {return true;}

    }
}