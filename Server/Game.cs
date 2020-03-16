using System;
using System.Timers;

namespace TechSimServer
{

    public class Game
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
            // Test Users
            PlayerData p1 = new PlayerData(); p1.username = "Aslan2142"; p1.password = "fAirfleNWWSe6XZhOfna9oWBiQORaYcp608hpihF3ei3+WFtSlN2zBZocUxw29r8aQ3Ntkyh2G1IdhgvgKuB/g==";
            PlayerData p2 = new PlayerData(); p2.username = "TestUser"; p2.password = "MydaiqSOqRi9U6kYGql18Vqw0GRTmPWRigBtCGdcHLJ9XGRdvQhO7lbmdeJbpAGfLs6jfKnimVtJ/LEsCWoDLg==";
            PlayerData p3 = new PlayerData(); p3.username = "Aslanek"; p3.password = "GKaL3SBa0SY+BM5sRUkgVHMRJ6tsSjc9kPU0Z0l1dVhL7g7CPJIvKAvxeMoOqg0C/P6Mb9vsoKquAkyAuOGRNA==";
            instance.data.playerData.Add(p1);
            instance.data.playerData.Add(p2);
            instance.data.playerData.Add(p3);
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