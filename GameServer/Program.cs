using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GameServer
{
    class Program
    {

        public static SaveData saveData = new SaveData();
        
        private static bool isRunning = false;

        public static bool autosaving = true;

        private static bool listeningToCmds = true;
        
        //commands list
        private delegate void ServerCommand(params string[] args);
        private static Dictionary<string, ServerCommand> serverCommands = new Dictionary<string, ServerCommand>();
        
        static void Main(string[] args)
        {
            Console.Title = "Game Server";
            isRunning = true;
            
            //Load game Data
            SetupGameData();

            //Initialize threads
            //Main Thread
            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            //Start Server
            Server.Start(8, 25565);
            
            
            //Autosave Thread
            Thread autosaveThead = new Thread(new ThreadStart(AutoSaveThread));
            autosaveThead.Start();
            
            //Commands Thread
            Thread commandsThread = new Thread(new ThreadStart(CommandsThread));
            commandsThread.Start();
            
        }

        private static void SetupGameData()
        {

            //Create save data if there is none, else load the data
            if (File.Exists(saveData.savePath))
            {
                
                Console.WriteLine($"Loading save File...");
                saveData.LoadGameData();
                Console.WriteLine($"Loading save File Complete!");

            }
            else
            {

                Console.WriteLine($"There is no save data located at {saveData.savePath}. Creating a new save file!");
                saveData.CreateSaveData();
                
            }
            
        }
        
        //Thread for tick loop and game logic updates
        private static void MainThread()
        {

            Console.WriteLine($"Main Thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
            DateTime nextLoop = DateTime.Now;

            while (isRunning)
            {

                while (nextLoop < DateTime.Now)
                {

                    GameLogic.Update();

                    nextLoop = nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (nextLoop > DateTime.Now)
                    {

                        Thread.Sleep(nextLoop - DateTime.Now);

                    }

                }

            }

        }
        
        //Thread for auto-Save (Base: every 30 secs)
        private static void AutoSaveThread()
        {
            
            DateTime nextLoop = DateTime.Now;

            while (autosaving)
            {

                while (nextLoop < DateTime.Now)
                {
                    
                    //Autosave here
                    Console.WriteLine("AutoSaving...");
                    saveData.SaveGameData();
                    Console.WriteLine("Autosaved!");
                    
                    nextLoop = nextLoop.AddMilliseconds(Constants.AUTOSAVE_INTERVAL_MS);

                    if (nextLoop > DateTime.Now)
                    {
                    
                        Thread.Sleep(nextLoop - DateTime.Now);
                    
                    }   
                    
                }

            }
            
        }

        private static void CommandsThread()
        {

            //Initialize server commands
            serverCommands = new Dictionary<string, ServerCommand>()
            {

                {"SAVE", CommandHandle.Save},
                {"HELP", CommandHandle.Help},
                {"STOP", CommandHandle.StopServer}

            };
            
            //Commands loop
            while (listeningToCmds)
            {

                //get input and parse cmd and args
                string input = Console.ReadLine() ?? "";
                string cmd = input.Split(" ")[0].ToUpper();
                string[] args = input.Length == cmd.Length ? new string[]{""} : input.Substring(cmd.Length+1).Split(" ");
            
                if (serverCommands.ContainsKey(cmd))
                {

                    Console.WriteLine();
                    serverCommands[cmd](args);

                }
                else
                {

                    CommandHandle.UnknownCommand();

                }

            }

        }
        
    }
}
