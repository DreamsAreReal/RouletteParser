using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace RouletteParser
{
    public class Handler
    {
        public Action<string> MessageReceived { get; }
        private readonly ILogger<CallConvThiscall> _logger;

        public Handler(ILogger<CallConvThiscall> logger)
        {
            _logger = logger;
            MessageReceived += OnMessageReceived;
        }



        private async void OnMessageReceived(string message)
        {
            var json = JsonConvert.DeserializeObject(message);
            if (json == null)
            {
                return;
            }

            try
            {
                var jObject = json as JObject;

                if (jObject?["type"]?.ToString() == "connection.kickout")
                {
                    await new Startup().Start();
                }




                if (jObject?["type"]?.ToString() == "roulette.winSpots"
                    && jObject?["args"]?["code"] != null
                    && jObject?["time"] != null)
                {
                    // Todo: send to my api.
                    _logger.LogInformation($"Result: {jObject?["args"]?["code"]}. Time: {jObject?["time"]}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Handler exception: {message}. Exception: {e.Message}");

            }
        }


    }
}
