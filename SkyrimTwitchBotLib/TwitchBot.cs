using System;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace SkyrimTwitchBotLib
{
    public class TwitchBot
    {
        public TwitchClient Client;
        bool _isSetup = false;

        public void Setup(SkyrimTwitchJsonConfigData config) {
            ConnectionCredentials credentials = new ConnectionCredentials(config.botUsername, config.accessToken);
            var clientOptions = new ClientOptions {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            Client = new TwitchClient(customClient);
            Client.Initialize(credentials, config.channelName);
            _isSetup = true;
        }

        public void Connect() {
            if (_isSetup)
                Client.Connect();
        }

        public void Disconnect() {
            Client.Disconnect();
        }

        public bool IsConnected { get => Client != null && Client.IsConnected; }
    }
}
