using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Numerics;
using System.Text;

namespace GameServer
{

    [Serializable]
    public enum PlayerTypes
    {
        
        Undefined = 1, Hunter = 2, Mage = 3
        
    }
    
    [Serializable]
    public class Player //This class is the representation of the player cursor and where the player is scene-wise
    {

        [NonSerialized] //dont serialize the id since it may change every time a player disconnects
        public int id;
        
        public string username;
        public string password;
        public PlayerTypes team = PlayerTypes.Undefined;

        [NonSerialized]
        public Vector3 cursorPosition;
        [NonSerialized] //Not sure if ths one should be serialized, but I don't think it should
        public int currentScene;

        public Player(int _id, string _username, string _password)
        {

            id = _id;
            username = _username;
            password = _password;
            cursorPosition = Vector3.Zero;

        }
        
        public Player(int _id, string _username, string _password, Vector3 _cursorPosition)
        {

            id = _id;
            username = _username;
            password = _password;
            cursorPosition = _cursorPosition;

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

        public void SetTeam(PlayerTypes _team)
        {

            team = _team;

        }

        public Player Connect(int _id)
        {

            id = _id;
            return this;

        }

        public void Update()
        {



        }
        
        public virtual bool IsMage () {return false;}
        public virtual bool IsHunter () {return false;}

    }
}
