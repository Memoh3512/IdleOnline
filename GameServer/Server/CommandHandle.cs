using System;

namespace GameServer
{
    public class CommandHandle
    {

        public static void Save(params string[] args)
        {
            
            Console.WriteLine("Saving game...");
            Program.saveData.SaveGameData();
            Console.WriteLine("Saved!");
            
        }

        public static void Help(params string[] args)
        {
            
            if (args[0] == "")
            {

                Console.WriteLine("*****Commands*****" + "\n" + 
                                  "Help: displays this list" + "\n" + 
                                  "Save: saves the game" + "\n" + 
                                  "Stop: stops the server" + "\n" + 
                                  //"" + "\n" + 
                                  "******************");

            }
            else
            {
                
                switch (args[0].ToUpper())
                {
                    
                    case "SAVE": Console.WriteLine($"Save: saves the game"); break;
                    case "HELP": Console.WriteLine($"Help: displays the list of commands\nHelp <command>: displays help on how to use command"); break;
                    case "STOP": Console.WriteLine($"Stop: closes the server"); break;

                    default: Console.WriteLine($"The command \"{args[0]}\" doesn't exist!"); break;
                    
                }
                
            }
            
        }

        public static void StopServer(params string[] args)
        {
            
            //TODO close server in a safer way
            Console.WriteLine("Closing server...");
            Environment.Exit(10);
            
            
        }

        public static void UnknownCommand()
        {
            
            Console.WriteLine("Unknown command! try \"Help\" for a list of available commands");
            
        }
        
    }
}