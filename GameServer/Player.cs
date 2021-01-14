using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class Player
    {

        public int id;
        public string username;

        public Vector3 cursorPosition;
        public int currentScene;
        
        public Player(int _id, string _username, Vector3 spawnPos)
        {

            id = _id;
            username = _username;
            cursorPosition = spawnPos;

        }

        public void SetCursorPosition(Vector3 newPos)
        {

            cursorPosition = newPos;

            ServerSend.PlayerCursorPosition(this);

        }

        public void SetCurrentScene(int newScene)
        {

            currentScene = newScene;
            ServerSend.PlayerChangedScene(this);

        }

        public void Update()
        {



        }

    }
}
