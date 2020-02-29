using System;
using System.Timers;

namespace TechSimServer
{

    class Game
    {

        // Singleton instance
        public static Game instance { get; protected set; }

        // Game data
        public GameData data { get; protected set; }

        public Game()
        {
            data = new GameData();
        }

        // Load game
        // If save doesn't exist, create new game
        public static bool Load()
        {
            // TO-DO

            instance = new Game();
            return true;
        }

        // Save game
        public static bool Save()
        {
            // TO-DO

            return true;
        }

        public Response HandleRequest(Request request)
        {
            // TO-DO

            return new Response(ResponseType.InvalidRequest);
        }
        
    }

}