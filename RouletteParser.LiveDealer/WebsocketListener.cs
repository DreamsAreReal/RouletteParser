using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RouletteParser.Core;
using RouletteParser.LiveDealer.Models;
using Websocket.Client;

namespace RouletteParser.LiveDealer
{
    public class WebsocketListener : IDisposable
    {
        private WebsocketClient _client;
        private const int InstanceIdLength = 5;
        private readonly ILogger<CallConvThiscall> _logger;
        
        public WebsocketListener(ILogger<CallConvThiscall> logger)
        {
            _logger = logger;
        }

        public async Task Receive(Action<string> messageReceived, SessionDataModel sessionData)
        {
            _client = new WebsocketClient(new Uri($"{Routes.WebsocketUrl}" +
                                                  $"&instance={RandomStringGenerator.Get(InstanceIdLength)}-{sessionData.UserId}-{Routes.GameId}" +
                                                  $"&tableConfig=" +
                                                  $"&EVOSESSIONID={sessionData.SessionId}" +
                                                  $"&client_version={Routes.ClientId}"));
            _logger.LogDebug($"Websocket open for session {sessionData.SessionId}, user id {sessionData.UserId}");
            
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
