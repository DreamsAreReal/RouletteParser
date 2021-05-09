using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using RouletteParser.Core;
using RouletteParser.LiveDealer.Models;
using Websocket.Client;

namespace RouletteParser.LiveDealer
{
    public class WebsocketListener : IDisposable
    {
        private readonly WebsocketClient _client;
        private const int InstanceIdLength = 5;
        
        public WebsocketListener(SessionDataModel sessionData)
        {
            _client = new WebsocketClient(new Uri($"{Routes.WebsocketUrl}" +
                                                  $"&instance={RandomStringGenerator.Get(InstanceIdLength)}-{sessionData.UserId}-{Routes.GameId}" +
                                                  $"&tableConfig=" +
                                                  $"&EVOSESSIONID={sessionData.SessionId}" +
                                                  $"&client_version={Routes.ClientId}"));

        }

        public async Task Receive(Action<string> messageReceived)
        {
            _client.MessageReceived.Subscribe(msg => messageReceived(msg.Text));
            await _client.Start();
        }

        public void Dispose()
        {
            _client.Stop(WebSocketCloseStatus.Empty,string.Empty);
            _client?.Dispose();
        }
    }
}
