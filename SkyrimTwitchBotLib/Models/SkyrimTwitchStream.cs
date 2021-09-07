using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyrimTwitchBotLib.Models {
    public class SkyrimTwitchStream {
        public static bool Exists(string streamName) => File.Exists(SkyrimTwitchBotFolder.GetStreamFilePath(streamName));
        public static void InitializeStream(string streamName) {
            var filepath = SkyrimTwitchBotFolder.GetStreamFilePath(streamName);
            var streamRedemptions = new List<SkyrimStreamRedemption>();
            var redemptionsJson = new Dictionary<string, List<SkyrimStreamRedemption>>();
            redemptionsJson["redemptions"] = streamRedemptions;
            SkyrimTwitchBotFolder.WriteToFile(redemptionsJson, filepath);
        }
    }
}
