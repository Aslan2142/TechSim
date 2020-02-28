using System;

namespace TechSimServer
{
    
    public class Program
    {
        
        // Program entry point
        private static void Main(string[] args)
        {
            // Start game instance
            Game.Load();

            // Get port
            Console.WriteLine("Enter Port Number (1000-65535)");
            Console.Write("> ");
            int port;
            try {
                port = Convert.ToInt32(Console.ReadLine());
                if (port < 1000 || port > 65535) throw new Exception();
            } catch (Exception) {
                Console.WriteLine("\nPort has to be a number between 1000 and 65535");
                return;
            }

            // Start server
            Server server = new Server(port);
            try {

                server.Start(true);

            } catch (Exception) {
                Console.WriteLine("\nServer error");
                return;
            }

            // Listen for commands
            while (true)
            {
                string command = Console.ReadLine();

                switch (command)
                {
                    // Stop server
                    case "stop":
                        Console.WriteLine("\nStopping the Server...");
                        server.Stop();
                        Environment.Exit(0);
                        break;
                    
                    // Unknown command
                    default:
                        Console.WriteLine("\nUnknown Command - " + command);
                        break;
                }
            }
        }
        
    }
    
}
