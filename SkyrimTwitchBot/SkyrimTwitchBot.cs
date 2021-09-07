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

namespace SkyrimTwitchBot
{
    public partial class SkyrimTwitchBotUI : Form
    {
        bool _initialized = false;
        static JsonConfigData? Configuration;

        public SkyrimTwitchBotUI()
        {
            InitializeComponent();
        }

        public void Log(string text) {
            RichText_EventLog.Text += text + "\n";
        }

        public void EnableUI() {
            if (SkyrimTwitchStream.Exists(this.Text_StreamName.Text)) {
                this.Button_NewStream.Enabled = false;
                this.Button_ContinueStream.Enabled = true;
            } else {
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

        private void OnInit(object sender, EventArgs e)
        {
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
            } else {
                this.Text_BotUsername.Text = Configuration.botUsername;
            }
            if (string.IsNullOrWhiteSpace(Configuration.channelName)) {
                Log("channelName is not configured");
                Log("Please ensure that config.json is in the same folder as SkyrimTwitchBot.exe and restart the application.");
                validConfiguration = false;
            } else {
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
            } else {
                this.Text_SkyrimDataFolder.Text = Configuration.skyrimDataFolder;
                SetupDataFolder();
            }
            if (! Directory.Exists(Configuration.skyrimDataFolder)) {
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
            } else if (_initialized) {
                EnableUI();
            }
        }
    }
}
