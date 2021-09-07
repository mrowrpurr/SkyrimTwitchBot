using System;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace MrowrPurr {
    class MrowrBot {
        TwitchClient client;

        public void Connect() {
            ConnectionCredentials credentials = new ConnectionCredentials(AuthorizationSecrets.UserName, AuthorizationSecrets.AccessToken);
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);
            client.Initialize(credentials, AuthorizationSecrets.ChannelName);

            client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnConnected += Client_OnConnected;

            client.Connect();
        }

        public void Disconnect() {
            client.Disconnect();
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            // Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!");
            // client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!");
        }

        IDictionary<string, DateTime> limiter = new Dictionary<string, DateTime>();

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            //if (e.ChatMessage.Message.Contains("badword"))
            //    client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
            Console.WriteLine($"OnMessage [{e.ChatMessage.DisplayName}] " + e.ChatMessage.Message);
            var username = e.ChatMessage.DisplayName;
            if (e.ChatMessage.Message == "!roll")
            {
                var now = DateTime.Now;
                if (limiter.ContainsKey(username))
                {
                    var lastRoll = limiter[username];
                    var duration = now - lastRoll;
                    if (duration.TotalSeconds > 5)
                    {
                        limiter[username] = now;
                        var filePath = @"C:\Steam\steamapps\common\Skyrim Special Edition\diceRolls.txt";
                        var writer = File.AppendText(filePath);
                        writer.WriteLine(e.ChatMessage.DisplayName);
                        writer.Close();
                    }
                }
                else
                {
                    var filePath = @"C:\Steam\steamapps\common\Skyrim Special Edition\diceRolls.txt";
                    var writer = File.AppendText(filePath);
                    writer.WriteLine(e.ChatMessage.DisplayName);
                    writer.Close();
                    limiter[username] = now;
                }
            }
        }

        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            //if (e.WhisperMessage.Username == "my_friend")
            //    client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
        }

        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            //if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
            //    client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
            //else
            //    client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
        }
    }
}
