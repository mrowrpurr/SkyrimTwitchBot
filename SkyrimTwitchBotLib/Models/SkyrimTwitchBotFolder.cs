using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SkyrimTwitchBotLib.Models {
    public class SkyrimTwitchBotFolder {
        public static string SKYRIM_DATA_FOLDER;
        public static string TWITCH_BOT_SUBFOLDER_NAME = "SkyrimTwitchBot";
        public static string REDEMPTION_TYPES_FILENAME = "RedemptionTypes.json";
        public static string VIEWER_SEPTIM_TRACKING_FILENAME = "Viewers.json";
        public static string PENDING_REDEMPTIONS_FILENAME = "PendingRedemptions.txt";
        public static string STREAMS_SUBFOLDER_NAME = "Streams";

        public static string TwitchBotDataDirectory { get => Path.Combine(SKYRIM_DATA_FOLDER, TWITCH_BOT_SUBFOLDER_NAME); }

        public static void Setup(string skyrimDataFolder) {
            SKYRIM_DATA_FOLDER = skyrimDataFolder;
            if (!Directory.Exists(TwitchBotDataDirectory))
                Directory.CreateDirectory(TwitchBotDataDirectory);
            SetupStreams();
            SetupRedemptionTypes();
            SetupViewerSeptimTracking();
            SetupPendingRedemptions();
        }

        public static void WriteToFile(object instance, string filename) {
            string text = JsonConvert.SerializeObject(instance);
            File.WriteAllText(filename, text);
        }

        public static string RedemptionTypesFilePath { get => Path.Combine(TwitchBotDataDirectory, REDEMPTION_TYPES_FILENAME); }
        public static Dictionary<string, bool> RedemptionTypes { get => JsonConvert.DeserializeObject<Dictionary<string, bool>>(File.ReadAllText(RedemptionTypesFilePath)); }

        public static void SetupRedemptionTypes() {
            // For now just make this a static list
            var redemptionTypes = new Dictionary<string, bool>() {
                { "!strip", true },
                { "!coc", true },
                { "!wishlist", true },
                { "!roll", true }
            };
            WriteToFile(redemptionTypes, RedemptionTypesFilePath);
        }

        public static string StreamsFolderName { get => Path.Combine(TwitchBotDataDirectory, STREAMS_SUBFOLDER_NAME); }
        public static void SetupStreams() {
            if (!Directory.Exists(StreamsFolderName))
                Directory.CreateDirectory(StreamsFolderName);
        }
        public static string GetStreamFilePath(string streamName) => Path.Combine(StreamsFolderName, streamName + ".json");

        public static string ViewerSeptimTrackingFilePath { get => Path.Combine(TwitchBotDataDirectory, VIEWER_SEPTIM_TRACKING_FILENAME);  }
        public static void SetupViewerSeptimTracking() {
            if (! File.Exists(ViewerSeptimTrackingFilePath)) {
                var viewers = new Dictionary<string, SkyrimViewer>();
                WriteToFile(viewers, ViewerSeptimTrackingFilePath);
            }
        }

        public static string PendingRedemptionsFilePath { get => Path.Combine(TwitchBotDataDirectory, PENDING_REDEMPTIONS_FILENAME); }
        public static void SetupPendingRedemptions() {
            if (! File.Exists(PendingRedemptionsFilePath)) {
                File.WriteAllText(PendingRedemptionsFilePath, ""); // touch the file
            }
        }
    }
}
