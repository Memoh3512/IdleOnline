using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class IdleMage
    {

        public static double Mana = 500;
        public static double ManaPerSecond = 1;
        

        public static void Update()
        {

            Mana += (ManaPerSecond / Constants.TICKS_PER_SEC);
            ServerSend.UpdateIdle(Mana, ManaPerSecond);
            //Console.Write("Idle updated");

        }


    }
}
