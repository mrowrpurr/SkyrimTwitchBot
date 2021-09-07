using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyrimTwitchBotLib.Models {
    public class PendingRedemptions {
        public static void AddPendingRedemption(string username, string redemptionType) {
            File.AppendAllText(SkyrimTwitchBotFolder.PendingRedemptionsFilePath, $"{username}:{redemptionType}\n");
        }
    }
}

