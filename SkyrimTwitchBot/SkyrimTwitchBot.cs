using SkyrimTwitchBotLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkyrimTwitchBotLib;
using System.IO;
using SkyrimTwitchBotLib.Models;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using TwitchLib.Communication.Events;
using TwitchLib.PubSub;

namespace SkyrimTwitchBot
{
    public partial class SkyrimTwitchBotUI : Form {

        int countOfChatMessagesSinceLastInfoMessage = 0;
        DateTime timeOfLastInfoMessage = DateTime.Now;

        string StreamName;
        TwitchBot bot = new TwitchBot();
        bool _initialized = false;
        static SkyrimTwitchJsonConfigData? Configuration;

        public SkyrimTwitchBotUI() {
            InitializeComponent();
        }

        delegate void SafeCallDelegate(string text);

        public void Log(string text) {
            RichText_EventLog.AppendText(text + "\n");
        }

        public void LogEvent(string text) {
            if (RichText_EventLog.InvokeRequired) {
                var caller = new SafeCallDelegate(LogEvent);
                RichText_EventLog.Invoke(caller, new object[] { text + "\n" });
            } else {
                RichText_EventLog.AppendText(text);
            }
        }
        public void ConnectivityLog(string text) {
            if (RichText_ConnectivityLog.InvokeRequired) {
                var caller = new SafeCallDelegate(ConnectivityLog);
                RichText_ConnectivityLog.Invoke(caller, new object[] { text + "\n" });
            } else {
                RichText_ConnectivityLog.AppendText(text);
            }
        }

        public void EnableUI() {
            if (SkyrimTwitchStream.Exists(this.Text_StreamName.Text)) {
                this.Button_NewStream.Enabled = false;
                this.Button_ContinueStream.Enabled = true;
            }
            else {
                this.Button_NewStream.Enabled = true;
                this.Button_ContinueStream.Enabled = false;
            }
        }

        public void DisableUI() {
            this.Button_NewStream.Enabled = false;
            this.Button_ContinueStream.Enabled = false;
        }

        public void SetupDataFolder() {
            SkyrimTwitchBotFolder.Setup(Configuration.skyrimDataFolder);
            _initialized = true;
        }

        public void LoadRedemptionTypes() {
            var types = SkyrimTwitchBotFolder.RedemptionTypes;
            this.ListBox_RedemptionTypes.Items.Clear();
            foreach (var type in types) {
                this.ListBox_RedemptionTypes.Items.Add(type.Key, type.Value);
            }
        }

        private void OnInit(object sender, EventArgs e) {
            // var pubsub = new TwitchPubSub();
            // pubsub.

            ///

            DateTime today = DateTime.Today;
            this.Text_StreamName.Text = $"{today.Month}-{today.Day}-{today.Year}";

            bool validConfiguration = true;

            Configuration = SkyrimTwitchBotConfig.ReadConfig();

            if (Configuration == null) {
                Log("Failed to load config.json");
                Log("Please ensure that config.json is in the same folder as SkyrimTwitchBot.exe and restart the application.");
                validConfiguration = false;
            }
            if (string.IsNullOrWhiteSpace(Configuration.botUsername)) {
                Log("botUsername is not configured");
                Log("Please ensure that config.json is in the same folder as SkyrimTwitchBot.exe and restart the application.");
                validConfiguration = false;
            }
            else {
                this.Text_BotUsername.Text = Configuration.botUsername;
            }
            if (string.IsNullOrWhiteSpace(Configuration.channelName)) {
                Log("channelName is not configured");
                Log("Please ensure that config.json is in the same folder as SkyrimTwitchBot.exe and restart the application.");
                validConfiguration = false;
            }
            else {
                this.Text_ChannelName.Text = Configuration.channelName;
            }
            if (string.IsNullOrWhiteSpace(Configuration.accessToken)) {
                Log("accessToken is not configured");
                Log("Please ensure that config.json is in the same folder as SkyrimTwitchBot.exe and restart the application.");
                validConfiguration = false;
            }
            if (string.IsNullOrWhiteSpace(Configuration.skyrimDataFolder)) {
                Log("skyrimDataFolder is not configured");
                Log("Please ensure that config.json is in the same folder as SkyrimTwitchBot.exe and restart the application.");
                validConfiguration = false;
            }
            else {
                this.Text_SkyrimDataFolder.Text = Configuration.skyrimDataFolder;
                SetupDataFolder();
            }
            if (!Directory.Exists(Configuration.skyrimDataFolder)) {
                Log("skyrimDataFolder was not found: " + Configuration.skyrimDataFolder);
                this.Text_SkyrimDataFolder.Text = "??? " + Configuration.skyrimDataFolder;
                validConfiguration = false;
            }
            if (validConfiguration) {
                EnableUI();
                LoadRedemptionTypes();
            }
        }

        private void Text_StreamName_TextChanged(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(this.Text_StreamName.Text)) {
                DisableUI();
            }
            else if (_initialized) {
                EnableUI();
            }
        }

        private void Button_NewStream_Click(object sender, EventArgs e) {
            if (bot.IsConnected) {
                // Should be disabled, no click for you.
            }
            else {
                StreamName = this.Text_StreamName.Text;
                Connect();
                this.Button_ContinueStream.Text = "Disconnect";
                this.Button_ContinueStream.Enabled = true;
                this.Button_NewStream.Enabled = false;
            }
        }

        private void Button_ContinueStream_Click(object sender, EventArgs e) {
            if (bot.IsConnected) {
                Disconnect();
                this.Button_ContinueStream.Text = "Continue Stream";
                EnableUI();
            }
            else {
                StreamName = this.Text_StreamName.Text;
                Connect();
                this.Button_ContinueStream.Text = "Disconnect";
                this.Button_ContinueStream.Enabled = true;
                this.Button_NewStream.Enabled = false;
            }
        }

        bool _clientEventHandlersConfigured = false;

        void Connect() {
            if (!_clientEventHandlersConfigured) {
                bot.Setup(Configuration);
                // bot.Client.on
                // Connectivity
                bot.Client.OnLog += Client_OnLog;
                bot.Client.OnConnected += Client_OnConnected;
                bot.Client.OnDisconnected += Client_OnDisconnected;
                // Message Handling
                bot.Client.OnMessageReceived += Client_OnMessageReceived;
                bot.Client.OnWhisperReceived += Client_OnWhisperReceived;
                // bot.Client.on
                _clientEventHandlersConfigured = true;
            }
            bot.Connect();
        }

        void Disconnect() {
            bot.Disconnect();
        }
        void Client_OnLog(object sender, OnLogArgs e) {
            ConnectivityLog($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}\n");
        }
        void Client_OnConnected(object sender, OnConnectedArgs e) {
            ConnectivityLog($"Connected to {e.AutoJoinChannel}");
        }
        void Client_OnDisconnected(object sender, OnDisconnectedEventArgs e) {
            ConnectivityLog($"Disconnected");
        }
        void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e) {
            LogEvent($"Whipser Received from {e.WhisperMessage.UserId} " + e.WhisperMessage.Username + ": " + e.WhisperMessage.Message);
            if (e.WhisperMessage.Message == "!septims") {
                int userSeptimCount = SkyrimViewerSeptimTracker.GetSeptimCount(e.WhisperMessage.Username);
                bot.Client.SendWhisper(e.WhisperMessage.Username, $"You currently have {userSeptimCount} septims!");
            } else {
                bot.Client.SendWhisper(e.WhisperMessage.Username, "I am Mrowr Purr's bot. Did you mean to ask me for !septims?");
            }
        }
        void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e) {
            countOfChatMessagesSinceLastInfoMessage += 1;
            if ((DateTime.Now - timeOfLastInfoMessage).Minutes > 40 && countOfChatMessagesSinceLastInfoMessage > 20) {
                countOfChatMessagesSinceLastInfoMessage = 0;
                timeOfLastInfoMessage = DateTime.Now;
                bot.Client.SendMessage(Configuration.channelName, "mrowrpTEA @MrowrPurr authors tutorials on Creation Kit and creating Skyrim Mods at \"Skyrim Scripting\": https://www.youtube.com/channel/UCS8mvo8o60dgPQe9WJRp2qQ mrowrpHI");
            }

            LogEvent($"Chat Message from {e.ChatMessage.UserId} " + e.ChatMessage.Username + ": " + e.ChatMessage.Message);

            // Just hack it for right now and check for literal strings, we'll revisit this :)
            int cost;
            string message = e.ChatMessage.Message;
            string username = e.ChatMessage.Username;
            SkyrimViewerSeptimTracker.TrackChatMessage(username);

            if (message.StartsWith("!")) {
                int currentSeptims = SkyrimViewerSeptimTracker.GetSeptimCount(username);
                if (username.ToLower() == "mrowrpurr" || username.ToLower() == "mrowrbot") {
                    currentSeptims = 10000;
                }
                if (message == "!septims") {
                    bot.Client.SendMessage(Configuration.channelName, $"{username} currently has {currentSeptims} septims");
                } else if (message == "!youtube") {
                    bot.Client.SendMessage(Configuration.channelName, "mrowrpTEA @MrowrPurr authors tutorials on Creation Kit and creating Skyrim Mods at \"Skyrim Scripting\": https://www.youtube.com/channel/UCS8mvo8o60dgPQe9WJRp2qQ mrowrpHI");
                } else if (message == "!mods") {
                    bot.Client.SendMessage(Configuration.channelName, "Mrowr Purr's Mod List: https://github.com/mrowrpurr/mods (it is a very short list)");
                } else if (message == "!game") {
                    bot.Client.SendMessage(Configuration.channelName, "Everyone earns 10 septims per minute | Chat messages earn more(limited) | List of available game commands: !game | Your current septims total: !septims | 100: !cheese | 250: !sweetrolls | 500: !wishlist | 750: !coc | 1000: !strip");
                } else if (message.StartsWith("!cheese")) {
                    cost = 100;
                    if (currentSeptims >= cost) {
                        SkyrimViewerSeptimTracker.DeductSeptims(username, cost);
                        PendingRedemptions.AddPendingRedemption(username, "!cheese");
                        bot.Client.SendMessage(Configuration.channelName, $"{username} is rolling all the cheese wheels!");
                    }
                    else {
                        bot.Client.SendMessage(Configuration.channelName, $"{username} does not have enough septims for " + message);
                    }
                }
                else if (message.StartsWith("!sweetroll")) {
                    cost = 250;
                    if (currentSeptims >= cost) {
                        SkyrimViewerSeptimTracker.DeductSeptims(username, cost);
                        PendingRedemptions.AddPendingRedemption(username, "!sweetroll");
                        bot.Client.SendMessage(Configuration.channelName, $"{username} is making it rain sweetrolls!");
                    }
                    else {
                        bot.Client.SendMessage(Configuration.channelName, $"{username} does not have enough septims for " + message);
                    }
                }
                else if (message.StartsWith("!wishlist")) {
                    cost = 500;
                    if (currentSeptims >= cost) {
                        SkyrimViewerSeptimTracker.DeductSeptims(username, cost);
                        PendingRedemptions.AddPendingRedemption(username, "!wishlist");
                        bot.Client.SendMessage(Configuration.channelName, $"{username} is gifting the player an item from the wishlist");
                    }
                    else {
                        bot.Client.SendMessage(Configuration.channelName, $"{username} does not have enough septims for " + message);
                    }
                }
                else if (message.StartsWith("!strip")) {
                    cost = 1000;
                    if (currentSeptims >= cost) {
                        SkyrimViewerSeptimTracker.DeductSeptims(username, cost);
                        PendingRedemptions.AddPendingRedemption(username, "!strip");
                        bot.Client.SendMessage(Configuration.channelName, $"{username} is stripping the player, oh my!");
                    }
                    else {
                        bot.Client.SendMessage(Configuration.channelName, $"{username} does not have enough septims for " + message);
                    }
                }
                else if (message.StartsWith("!coc")) {
                    if (message == "!coc") {
                        bot.Client.SendMessage(Configuration.channelName, $"!coc requires a valid location to teleport to");
                    } else {
                        string[] parts = message.Split(" ");
                        if (parts.Length != 2) {
                            bot.Client.SendMessage(Configuration.channelName, $"!coc requires a valid location to teleport to");
                        } else {
                            cost = 750;
                            if (currentSeptims >= cost) {
                                string cell = parts[1];
                                SkyrimViewerSeptimTracker.DeductSeptims(username, cost);
                                PendingRedemptions.AddPendingRedemption(username, $"!coc|{cell}");
                                bot.Client.SendMessage(Configuration.channelName, $"{username} is teleporting the player to {cell}!");
                            }
                            else {
                                bot.Client.SendMessage(Configuration.channelName, $"{username} does not have enough septims for " + message);
                            }
                        }
                    }
                }
            }
        }
    }
}
