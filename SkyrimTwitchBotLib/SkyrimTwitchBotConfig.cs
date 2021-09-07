using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyrimTwitchBotLib {
    public class JsonConfigData {
        public string botUsername { get; set; }
        public string channelName { get; set; }
        public string accessToken { get; set; }
        public string skyrimDataFolder { get; set; }
    }

   public class SkyrimTwitchBotConfig {

        public static string CONFIG_FILENAME = "config.json";

        public static JsonConfigData? ReadConfig() {
            try {
                using var reader = new StreamReader(CONFIG_FILENAME);
                return JsonConvert.DeserializeObject<JsonConfigData>(reader.ReadToEnd());
            } catch {
                return null;
            }
        }
    }
}
