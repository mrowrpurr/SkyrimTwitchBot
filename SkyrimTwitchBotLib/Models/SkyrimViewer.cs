using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyrimTwitchBotLib.Models
{
    public class SkyrimViewer
    {
        public string Username { get; set; }
        public int SeptimsSpent { get; set; }
        public int ValidChatMessagesReceived { get; set; }
        public DateTime? LastMessageReceived { get; set; } // This isn't actually LastMessageReceived, it only updates every 5 minutes, rename, it's a throttling time
    }
}

