using System;
using System.IO;
using System.Text.Json;

namespace TechSimServer
{

    [Serializable]
    public class Config
    {

        public string GAME_SAVE_LOCATION { get; set; }
        public string AUTHORIZATION_INCORRECT_PASSWORD_MESSAGE { get; set; }
        public string AUTHORIZATION_WRONG_USERNAME_LENGTH_MESSAGE { get; set; }
        public string AUTHORIZATION_WRONG_PASSWORD_LENGTH_MESSAGE { get; set; }
        
        public int MINIMUM_USERNAME_LENGTH { get; set; }
        public int MAXIMUM_USERNAME_LENGTH { get; set; }

        public static Config instance;

        public static bool Load()
        {
            try
            {
                string configJson = File.ReadAllText(Consts.SERVER_CONFIG_LOCATION);
                instance = JsonSerializer.Deserialize(configJson, typeof(Config)) as Config;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

}