using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyrimTwitchBotLib.Models
{
    public class SkyrimViewerSeptimTracker
    {
        // public static DateTime STREAM_START_TIME = DateTime.Parse("9/7/2021 6:00:00 PM"); // Placeholder for real logic and stuffs!

        // FOR TESTING
        public static DateTime STREAM_START_TIME = DateTime.Parse("9/7/2021 3:00:00 PM"); // Placeholder for real logic and stuffs!

        public static Dictionary<string, SkyrimViewer> CurrentViewerStats { get => JsonConvert.DeserializeObject<Dictionary<string, SkyrimViewer>>(File.ReadAllText(SkyrimTwitchBotFolder.ViewerSeptimTrackingFilePath)); }

        public static void SaveViewerStats(Dictionary<string, SkyrimViewer> stats) {
            SkyrimTwitchBotFolder.WriteToFile(stats, SkyrimTwitchBotFolder.ViewerSeptimTrackingFilePath);
        }

        public static int GetSeptimCount(string username) {
            var allUsers = CurrentViewerStats;
            SkyrimViewer thisUser = null;
            if (allUsers.ContainsKey(username)) {
                thisUser = allUsers[username];
            }
            if (thisUser != null) {
                int currentSeptims = GetCurrentSeptimsIfNoneSpent();
                int userDeductedSeptims = thisUser.SeptimsSpent;
                int extraSeptims = thisUser.ValidChatMessagesReceived * 10;
                int total = currentSeptims - userDeductedSeptims + extraSeptims;
                return total;
            } else {
                return GetCurrentSeptimsIfNoneSpent();
            }
        }

        // Threading is going to make this sad lol. It won't track perfectly because it'll overwrite the file and stuff.
        public static void TrackChatMessage(string username) {
            var allUsers = CurrentViewerStats;
            SkyrimViewer thisUser = null;
            if (allUsers.ContainsKey(username)) {
                thisUser = allUsers[username];
            }
            if (thisUser == null) {
                var viewer = new SkyrimViewer {
                    Username = username,
                    LastMessageReceived = DateTime.Now,
                    SeptimsSpent = 0,
                    ValidChatMessagesReceived = 1
                };
                allUsers[username] = viewer;
                SaveViewerStats(allUsers);
            } else {
                var lastMessage = thisUser.LastMessageReceived;
                if (lastMessage == null) {
                    lastMessage = DateTime.Now;
                }
                var timeSinceLastMessage = DateTime.Now - thisUser.LastMessageReceived;
                if (timeSinceLastMessage.GetValueOrDefault().Minutes >= 5) {
                    thisUser.ValidChatMessagesReceived += 1;
                    thisUser.LastMessageReceived = DateTime.Now;
                    SaveViewerStats(allUsers);
                }
            }
        }

        public static void DeductSeptims(string username, int cost) {
            var allUsers = CurrentViewerStats;
            var thisUser = allUsers[username];
            if (thisUser != null) {
                thisUser.SeptimsSpent += cost;
                SaveViewerStats(allUsers);
            }
        }

        public static int GetCurrentSeptimsIfNoneSpent() {
            var duration = DateTime.Now - STREAM_START_TIME;
            return duration.Minutes * 10;
        }
    }
}
