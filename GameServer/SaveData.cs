using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace GameServer
{
    public class SaveData
    {

        public string savePath = "Save.sav";

        public DateTime lastSaved = DateTime.Now;
        
        //Username, Password
        public static Dictionary<string, string> users;
        
        //Idle-related objects
        public IdleMage idleMage;

        public SaveData () {}
        
        public void SaveGameData()
        {
            try
            {

                //Saving all data objects used for the game
                using (Stream stream = File.Open(savePath,FileMode.Append))
                {
                    
                    //setupping formatter for binary save
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                
                    //saving user credentials
                    binaryFormatter.Serialize(stream, users);
                
                    //saving Mage stuff
                    binaryFormatter.Serialize(stream, idleMage);

                }
                
                lastSaved = DateTime.Now;

            }
            catch (Exception e)
            {
                
                Console.WriteLine($"Error writing to save File: {e}");
                
            }

        }

        public void LoadGameData()
        {
            
            try
            {

                using (Stream stream = File.Open(savePath, FileMode.Open))
                {
                    //setupping formatter for loading data
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    //loading user credentials
                    users = (Dictionary<string,string>)binaryFormatter.Deserialize(stream);
                    
                    //loading Mage stuff
                    idleMage = (IdleMage)binaryFormatter.Deserialize(stream);
                    
                }

            }
            catch (Exception e)
            {
                
                Console.WriteLine($"Error reading from save File: {e}");
                
            } 
            
        }

        //creates fresh save data
        public void CreateSaveData()
        {
            
            users = new Dictionary<string, string>();
            idleMage = new IdleMage();
            
            SaveGameData();

        }
        

    }
}