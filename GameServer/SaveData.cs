using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace GameServer
{
    public class SaveData
    {

        public string savePath = "Save.sav";

        public DateTime lastSaved = DateTime.Now;
        
        //Username, Password
        public Dictionary<string, Player> users;
        
        //Idle-related objects
        public IdleMage idleMage;

        public SaveData () {}
        
        #region FileSaving
        public void SaveGameData()
        {
            
            FileStream stream = new FileStream(savePath, FileMode.Create);
            
            try
            {

                //setupping formatter for binary save
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                //saving user credentials
                binaryFormatter.Serialize(stream, users);

                //saving Mage stuff
                binaryFormatter.Serialize(stream, idleMage);

                lastSaved = DateTime.Now;

            }
            catch (Exception e)
            {

                Console.WriteLine($"Error writing to save File: {e}");

            }
            finally
            {
                
                stream.Close();
                
            }

        }

        public void LoadGameData()
        {

            FileStream stream = new FileStream(savePath, FileMode.Open);
            
            try
            {

                //setupping formatter for loading data
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                //loading user credentials
                users = (Dictionary<string, Player>) binaryFormatter.Deserialize(stream);

                //loading Mage stuff
                idleMage = (IdleMage) binaryFormatter.Deserialize(stream);

                Console.WriteLine(
                    $"\nSave file stuff:\nUsers size: {users.Count}\nUser #1: {users.FirstOrDefault().Key}\nIdleMage, ManaPerSec: {idleMage.ManaPerSecond}\n");

            }
            catch (Exception e)
            {

                Console.WriteLine($"Error reading from save File: {e}");

            }
            finally
            {

                stream.Close();

            }
            
        }

        //creates fresh save data
        public void CreateSaveData()
        {
            
            users = new Dictionary<string, Player>();
            idleMage = new IdleMage();
            
            SaveGameData();

        }

        #endregion
        
        #region Access

        public Player AssignPlayer( Player ply,PlayerTypes type)
        {

            switch (type)
            {
                
                case PlayerTypes.Hunter:
                    return new Hunter(ply);
                    break;
                
                case PlayerTypes.Mage:
                    return new Mage(ply);
                    break;

            }

            //if something went wrong
            Console.WriteLine("Something went wrong when choosing a team, please report to dev thanks ^^!");
            return ply;

        }

        public Player NewPlayer(int id, string username, string password)
        {
            
            //Check if player exists
            if (users.ContainsKey(username))
            {

                return users[username].Connect(id);

            }
            else
            {
                
                //creates new player with supplied stuff
                return new Player(id, username, password);

            }
            
        }

        public List<Mage> GetAllMages()
        {

            return users.Values.OfType<Mage>().ToList();

        }

        public List<Hunter> GetAllHunters()
        {

            return users.Values.OfType<Hunter>().ToList();

        }
        
        #endregion

    }
}