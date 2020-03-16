using System;
using System.Collections.Generic;

namespace TechSimServer
{

    public class GameData
    {

        public int currentTime = 0; // How many in-game hours passed since start of the game (1.1.1980)
        public int timeSpeed = 1; // By how many hours will time move every tick
        public int tickTime = 5000; // Time for tick (in milliseconds)

        public List<PlayerData> playerData = new List<PlayerData>(); // All player information

    }

}