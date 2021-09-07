
namespace SkyrimTwitchBot
{
    partial class SkyrimTwitchBotUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Button_NewStream = new System.Windows.Forms.Button();
            this.Label_BotUsername = new System.Windows.Forms.Label();
            this.Text_BotUsername = new System.Windows.Forms.TextBox();
            this.Text_ChannelName = new System.Windows.Forms.TextBox();
            this.Label_ChannelName = new System.Windows.Forms.Label();
            this.RichText_EventLog = new System.Windows.Forms.RichTextBox();
            this.Label_TwitchEventLog = new System.Windows.Forms.Label();
            this.Label_ConnectivityLog = new System.Windows.Forms.Label();
            this.RichText_ConnectivityLog = new System.Windows.Forms.RichTextBox();
            this.ListBox_RedemptionTypes = new System.Windows.Forms.CheckedListBox();
            this.Label_RedemptionTypes = new System.Windows.Forms.Label();
            this.Text_NewRedemptionType = new System.Windows.Forms.TextBox();
            this.Label_AddRedemptionType = new System.Windows.Forms.Label();
            this.Button_AddRedemptionType = new System.Windows.Forms.Button();
            this.Text_SkyrimDataFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Text_StreamName = new System.Windows.Forms.TextBox();
            this.Label_StreamName = new System.Windows.Forms.Label();
            this.Button_ContinueStream = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Button_NewStream
            // 
            this.Button_NewStream.Enabled = false;
            this.Button_NewStream.Location = new System.Drawing.Point(555, 52);
            this.Button_NewStream.Name = "Button_NewStream";
            this.Button_NewStream.Size = new System.Drawing.Size(117, 30);
            this.Button_NewStream.TabIndex = 0;
            this.Button_NewStream.Text = "New Stream";
            this.Button_NewStream.UseVisualStyleBackColor = true;
            this.Button_NewStream.Click += new System.EventHandler(this.Button_NewStream_Click);
            // 
            // Label_BotUsername
            // 
            this.Label_BotUsername.AutoSize = true;
            this.Label_BotUsername.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_BotUsername.Location = new System.Drawing.Point(12, 15);
            this.Label_BotUsername.Name = "Label_BotUsername";
            this.Label_BotUsername.Size = new System.Drawing.Size(87, 15);
            this.Label_BotUsername.TabIndex = 1;
            this.Label_BotUsername.Text = "Bot Username";
            // 
            // Text_BotUsername
            // 
            this.Text_BotUsername.Enabled = false;
            this.Text_BotUsername.Location = new System.Drawing.Point(105, 12);
            this.Text_BotUsername.Name = "Text_BotUsername";
            this.Text_BotUsername.Size = new System.Drawing.Size(138, 23);
            this.Text_BotUsername.TabIndex = 2;
            // 
            // Text_ChannelName
            // 
            this.Text_ChannelName.Enabled = false;
            this.Text_ChannelName.Location = new System.Drawing.Point(341, 12);
            this.Text_ChannelName.Name = "Text_ChannelName";
            this.Text_ChannelName.Size = new System.Drawing.Size(146, 23);
            this.Text_ChannelName.TabIndex = 4;
            // 
            // Label_ChannelName
            // 
            this.Label_ChannelName.AutoSize = true;
            this.Label_ChannelName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_ChannelName.Location = new System.Drawing.Point(249, 15);
            this.Label_ChannelName.Name = "Label_ChannelName";
            this.Label_ChannelName.Size = new System.Drawing.Size(87, 15);
            this.Label_ChannelName.TabIndex = 3;
            this.Label_ChannelName.Text = "Channel Name";
            // 
            // RichText_EventLog
            // 
            this.RichText_EventLog.Location = new System.Drawing.Point(12, 70);
            this.RichText_EventLog.Name = "RichText_EventLog";
            this.RichText_EventLog.ReadOnly = true;
            this.RichText_EventLog.Size = new System.Drawing.Size(525, 189);
            this.RichText_EventLog.TabIndex = 5;
            this.RichText_EventLog.Text = "";
            // 
            // Label_TwitchEventLog
            // 
            this.Label_TwitchEventLog.AutoSize = true;
            this.Label_TwitchEventLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_TwitchEventLog.Location = new System.Drawing.Point(248, 52);
            this.Label_TwitchEventLog.Name = "Label_TwitchEventLog";
            this.Label_TwitchEventLog.Size = new System.Drawing.Size(62, 15);
            this.Label_TwitchEventLog.TabIndex = 6;
            this.Label_TwitchEventLog.Text = "Event Log";
            // 
            // Label_ConnectivityLog
            // 
            this.Label_ConnectivityLog.AutoSize = true;
            this.Label_ConnectivityLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_ConnectivityLog.Location = new System.Drawing.Point(192, 278);
            this.Label_ConnectivityLog.Name = "Label_ConnectivityLog";
            this.Label_ConnectivityLog.Size = new System.Drawing.Size(163, 15);
            this.Label_ConnectivityLog.TabIndex = 8;
            this.Label_ConnectivityLog.Text = "Twitch Bot Connectivity Log";
            // 
            // RichText_ConnectivityLog
            // 
            this.RichText_ConnectivityLog.Location = new System.Drawing.Point(12, 296);
            this.RichText_ConnectivityLog.Name = "RichText_ConnectivityLog";
            this.RichText_ConnectivityLog.ReadOnly = true;
            this.RichText_ConnectivityLog.Size = new System.Drawing.Size(525, 205);
            this.RichText_ConnectivityLog.TabIndex = 7;
            this.RichText_ConnectivityLog.Text = "";
            // 
            // ListBox_RedemptionTypes
            // 
            this.ListBox_RedemptionTypes.Enabled = false;
            this.ListBox_RedemptionTypes.FormattingEnabled = true;
            this.ListBox_RedemptionTypes.Location = new System.Drawing.Point(555, 116);
            this.ListBox_RedemptionTypes.Name = "ListBox_RedemptionTypes";
            this.ListBox_RedemptionTypes.Size = new System.Drawing.Size(249, 328);
            this.ListBox_RedemptionTypes.TabIndex = 9;
            // 
            // Label_RedemptionTypes
            // 
            this.Label_RedemptionTypes.AutoSize = true;
            this.Label_RedemptionTypes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_RedemptionTypes.Location = new System.Drawing.Point(571, 98);
            this.Label_RedemptionTypes.Name = "Label_RedemptionTypes";
            this.Label_RedemptionTypes.Size = new System.Drawing.Size(204, 15);
            this.Label_RedemptionTypes.TabIndex = 10;
            this.Label_RedemptionTypes.Text = "Skyrim In-Game Redemption Types";
            // 
            // Text_NewRedemptionType
            // 
            this.Text_NewRedemptionType.Location = new System.Drawing.Point(555, 474);
            this.Text_NewRedemptionType.Name = "Text_NewRedemptionType";
            this.Text_NewRedemptionType.Size = new System.Drawing.Size(184, 23);
            this.Text_NewRedemptionType.TabIndex = 11;
            // 
            // Label_AddRedemptionType
            // 
            this.Label_AddRedemptionType.AutoSize = true;
            this.Label_AddRedemptionType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_AddRedemptionType.Location = new System.Drawing.Point(555, 456);
            this.Label_AddRedemptionType.Name = "Label_AddRedemptionType";
            this.Label_AddRedemptionType.Size = new System.Drawing.Size(130, 15);
            this.Label_AddRedemptionType.TabIndex = 12;
            this.Label_AddRedemptionType.Text = "Add Redemption Type";
            // 
            // Button_AddRedemptionType
            // 
            this.Button_AddRedemptionType.Enabled = false;
            this.Button_AddRedemptionType.Location = new System.Drawing.Point(745, 467);
            this.Button_AddRedemptionType.Name = "Button_AddRedemptionType";
            this.Button_AddRedemptionType.Size = new System.Drawing.Size(59, 30);
            this.Button_AddRedemptionType.TabIndex = 13;
            this.Button_AddRedemptionType.Text = "Add";
            this.Button_AddRedemptionType.UseVisualStyleBackColor = true;
            // 
            // Text_SkyrimDataFolder
            // 
            this.Text_SkyrimDataFolder.Enabled = false;
            this.Text_SkyrimDataFolder.Location = new System.Drawing.Point(216, 517);
            this.Text_SkyrimDataFolder.Name = "Text_SkyrimDataFolder";
            this.Text_SkyrimDataFolder.Size = new System.Drawing.Size(588, 23);
            this.Text_SkyrimDataFolder.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 520);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Skyrim Special Edition Data Folder";
            // 
            // Text_StreamName
            // 
            this.Text_StreamName.Location = new System.Drawing.Point(595, 12);
            this.Text_StreamName.Name = "Text_StreamName";
            this.Text_StreamName.Size = new System.Drawing.Size(200, 23);
            this.Text_StreamName.TabIndex = 18;
            this.Text_StreamName.TextChanged += new System.EventHandler(this.Text_StreamName_TextChanged);
            // 
            // Label_StreamName
            // 
            this.Label_StreamName.AutoSize = true;
            this.Label_StreamName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label_StreamName.Location = new System.Drawing.Point(505, 15);
            this.Label_StreamName.Name = "Label_StreamName";
            this.Label_StreamName.Size = new System.Drawing.Size(84, 15);
            this.Label_StreamName.TabIndex = 17;
            this.Label_StreamName.Text = "Stream Name";
            // 
            // Button_ContinueStream
            // 
            this.Button_ContinueStream.Enabled = false;
            this.Button_ContinueStream.Location = new System.Drawing.Point(678, 52);
            this.Button_ContinueStream.Name = "Button_ContinueStream";
            this.Button_ContinueStream.Size = new System.Drawing.Size(117, 30);
            this.Button_ContinueStream.TabIndex = 19;
            this.Button_ContinueStream.Text = "Continue Stream";
            this.Button_ContinueStream.UseVisualStyleBackColor = true;
            this.Button_ContinueStream.Click += new System.EventHandler(this.Button_ContinueStream_Click);
            // 
            // SkyrimTwitchBotUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 553);
            this.Controls.Add(this.Button_ContinueStream);
            this.Controls.Add(this.Text_StreamName);
            this.Controls.Add(this.Label_StreamName);
            this.Controls.Add(this.Text_SkyrimDataFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Button_AddRedemptionType);
            this.Controls.Add(this.Label_AddRedemptionType);
            this.Controls.Add(this.Text_NewRedemptionType);
            this.Controls.Add(this.Label_RedemptionTypes);
            this.Controls.Add(this.ListBox_RedemptionTypes);
            this.Controls.Add(this.Label_ConnectivityLog);
            this.Controls.Add(this.RichText_ConnectivityLog);
            this.Controls.Add(this.Label_TwitchEventLog);
            this.Controls.Add(this.RichText_EventLog);
            this.Controls.Add(this.Text_ChannelName);
            this.Controls.Add(this.Label_ChannelName);
            this.Controls.Add(this.Text_BotUsername);
            this.Controls.Add(this.Label_BotUsername);
            this.Controls.Add(this.Button_NewStream);
            this.Name = "SkyrimTwitchBotUI";
            this.Text = "Skyrim Twitch Bot";
            this.Load += new System.EventHandler(this.OnInit);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_NewStream;
        private System.Windows.Forms.Label Label_BotUsername;
        private System.Windows.Forms.TextBox Text_BotUsername;
        private System.Windows.Forms.TextBox Text_ChannelName;
        private System.Windows.Forms.Label Label_ChannelName;
        private System.Windows.Forms.RichTextBox RichText_EventLog;
        private System.Windows.Forms.Label Label_TwitchEventLog;
        private System.Windows.Forms.Label Label_ConnectivityLog;
        private System.Windows.Forms.RichTextBox RichText_ConnectivityLog;
        private System.Windows.Forms.CheckedListBox ListBox_RedemptionTypes;
        private System.Windows.Forms.Label Label_RedemptionTypes;
        private System.Windows.Forms.TextBox Text_NewRedemptionType;
        private System.Windows.Forms.Label Label_AddRedemptionType;
        private System.Windows.Forms.Button Button_AddRedemptionType;
        private System.Windows.Forms.TextBox Text_SkyrimDataFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Text_StreamName;
        private System.Windows.Forms.Label Label_StreamName;
        private System.Windows.Forms.Button Button_ContinueStream;
    }
}

