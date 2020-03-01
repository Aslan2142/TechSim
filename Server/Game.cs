using System;
using System.Timers;

namespace TechSimServer
{

    class Game
    {

        public static Game instance { get; protected set; } // Singleton instance

        public GameData data { get; protected set; } // Game data

        private Timer tickTimer = new Timer();

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

        // Start game tick timer
        public void Start()
        {
            tickTimer.Interval = data.tickTime;
            tickTimer.AutoReset = true;
            tickTimer.Elapsed += Tick;

            tickTimer.Start();
        }

        // Stop game tick timer
        public void Stop()
        {
            tickTimer.Stop();
        }

        // Game tick
        // This is called automatically by tickTimer after game started
        private void Tick(object sender, ElapsedEventArgs e)
        {
            data.currentTime += data.timeSpeed;
        }

        public Response HandleRequest(Request request)
        {
            switch (request.Type)
            {
                case RequestType.GetTime:
                    return new Response(ResponseType.Time, Helpers.GetTimeDate(data.currentTime));
            }

            return new Response(ResponseType.InvalidRequest);
        }

    }

}