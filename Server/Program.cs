using System;

namespace TechSimServer
{

    public class Program
    {

        // Program entry point
        private static void Main(string[] args)
        {
            // Load Config
            if (!Config.Load())
            {
                Console.WriteLine("Can't load config file. Exiting...");
                return;
            }
            System.Console.WriteLine(Config.instance.AUTHORIZATION_WRONG_PASSWORD_LENGTH_MESSAGE);
            // Load and start game instance
            if (!Game.Load())
            {
                Console.WriteLine("Game could not be loaded. Exiting...");
                return;
            }
            Game.instance.Start();

            // Get port
            Console.WriteLine("Enter Port Number (1000-65535)");
            Console.Write("> ");
            int port;
            try
            {
                port = Convert.ToInt32(Console.ReadLine());
                if (port < 1000 || port > 65535) throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("\nPort has to be a number between 1000 and 65535");
                return;
            }

            // Start server
            Server server = new Server(port, 1024);
            try
            {
                server.Start(true);
            }
            catch (Exception)
            {
                Console.WriteLine("\nServer error");
                return;
            }

            // Listen for commands
            bool gameRunning = true;
            while (gameRunning)
            {
                string command = Console.ReadLine().ToLower();

                switch (command)
                {
                    // Stop server
                    case "stop":
                        gameRunning = false;
                        break;
                    
                    // Save game data
                    case "save":
                        Game.Save();
                        break;

                    // Unknown command
                    default:
                        Console.WriteLine("\nUnknown Command - " + command);
                        break;
                }
            }

            // Stop server
            Console.WriteLine("\nStopping the Server...");
            server.Stop();

            // Stop and save game instance
            Game.instance.Stop();
            bool saving = true;
            while (saving)
            {
                if (!Game.Save())
                {
                    Console.WriteLine("Game could not be saved. Retry?");
                    Console.Write("(yes/no) > ");

                    if (Console.ReadLine() == "no")
                    {
                        saving = false;
                    }
                }
                else
                {
                    saving = false;
                }
            }
        }

    }

}
