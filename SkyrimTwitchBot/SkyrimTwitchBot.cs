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

namespace SkyrimTwitchBot
{
    public partial class SkyrimTwitchBotUI : Form {
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
                // Connectivity
                bot.Client.OnLog += Client_OnLog;
                bot.Client.OnConnected += Client_OnConnected;
                bot.Client.OnDisconnected += Client_OnDisconnected;
                // Message Handling
                bot.Client.OnMessageReceived += Client_OnMessageReceived;
                bot.Client.OnWhisperReceived += Client_OnWhisperReceived;
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
            LogEvent($"Whipser Received " + e.WhisperMessage.Username + ": " + e.WhisperMessage.Message);
        }
        void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e) {
            LogEvent($"Chat Message " + e.ChatMessage.Username + ": " + e.ChatMessage.Message);
        }
    }
}
