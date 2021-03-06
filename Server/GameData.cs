using System;
using System.Collections.Generic;

namespace TechSimServer
{

    [Serializable]
    public class GameData
    {

        public int currentTime { get; set; } = 0; // How many in-game hours passed since start of the game (1.1.1980)
        public int timeSpeed { get; set; } = 1; // By how many hours will time move every tick
        public int tickTime { get; set; } = 5000; // Time for tick (in milliseconds)

        public List<PlayerData> playerData { get; set; } = new List<PlayerData>(); // All player information

    }

}