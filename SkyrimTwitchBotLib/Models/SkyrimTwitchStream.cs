using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyrimTwitchBotLib.Models {
    public class SkyrimTwitchStream {
        public static bool Exists(string streamName) => File.Exists(SkyrimTwitchBotFolder.GetStreamFilePath(streamName));
    }
}
