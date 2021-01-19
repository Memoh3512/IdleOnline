using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class GameLogic
    {

        public static void Update()
        {

            foreach (Client client in Server.clients.Values)
            {
                client.player?.Update();

            }

            //update le idle
            Program.saveData.idleMage.Update();

            ThreadManager.UpdateMain();

        }

    }
}
