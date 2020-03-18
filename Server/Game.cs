using System;
using System.IO;
using System.Timers;
using System.Text.Json;

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
            try
            {
                instance = new Game();
                if (File.Exists(Config.instance.GAME_SAVE_LOCATION))
                {
                    string gameDataJson = File.ReadAllText(Config.instance.GAME_SAVE_LOCATION);
                    instance.data = JsonSerializer.Deserialize(gameDataJson, typeof(GameData)) as GameData;
                } else {
                    Console.WriteLine("Game save not found. Creating new game data...");
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Save game
        public static bool Save()
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
                string gameDataJson = JsonSerializer.Serialize(instance.data, typeof(GameData), options);
                File.WriteAllText(Config.instance.GAME_SAVE_LOCATION, gameDataJson);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
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

        public Response HandleRequest(Request request, PlayerData player)
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