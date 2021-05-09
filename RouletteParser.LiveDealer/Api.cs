using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RouletteParser.LiveDealer.Exceptions;
using RouletteParser.LiveDealer.Models;

namespace RouletteParser.LiveDealer
{
    public class Api
    {
        private HttpClient _client;
        private readonly ILogger<CallConvThiscall> _logger;

        
        public Api(ILogger<CallConvThiscall> logger)
        {
            _logger = logger;
        }

        public void SetClient(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Method add cookie in cookie storage.
        /// </summary>
        /// <param name="url">Url from site casino for authorization on live dealer</param>
        public async Task Authorization(string url)
        {
            if (_client == null)
            {
                throw new NullReferenceException("Client can't be null");
            }

            _logger.LogDebug($"Authorization on {nameof(LiveDealer)} url {url}");
            await _client.GetAsync(url);
        }

        /// <summary>
        /// Get data from live dealer for get access to websocket
        /// </summary>
        /// <returns>Data for get access to websocket</returns>
        public async Task<SessionDataModel> GetAccessSessionData()
        {
            if (_client == null)
            {
                throw new NullReferenceException("Client can't be null");
            }

            _logger.LogDebug($"Getting data for websocket access");
            var answer =
                await (await _client.GetAsync(Routes.SetupDevice))
                    .Content.ReadAsStringAsync();
            SessionDataModel sessionData;
            try
            {
                sessionData = JsonConvert.DeserializeObject<SessionDataModel>(answer);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"On getting data throw exception {ex}");
                throw new SessionDataParseException(ex.ToString());
            }
            _logger.LogDebug($"Data for websocket access is receive");
            return sessionData;
        }
    }
}