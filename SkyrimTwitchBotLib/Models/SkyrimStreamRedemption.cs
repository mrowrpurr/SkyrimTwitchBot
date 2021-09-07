using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyrimTwitchBotLib.Models {
    public class SkyrimStreamRedemption {
        public string Username { get; set; }
        public string RedemptionName { get; set; }
        public DateTime RedemptionTime { get; set; }
        public string RedemptionCommand { get; set; }
        public int RedemptionSeptims { get; set; }
    }
}
