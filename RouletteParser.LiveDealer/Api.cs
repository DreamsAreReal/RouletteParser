using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RouletteParser.LiveDealer.Exceptions;
using RouletteParser.LiveDealer.Models;

namespace RouletteParser.LiveDealer
{
    public class Api
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initialization api
        /// </summary>
        /// <param name="client">Client with authorization cookie from site casino</param>
        public Api(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Method add cookie in cookie storage.
        /// </summary>
        /// <param name="url">Url from site casino for authorization on live dealer</param>
        public async Task Authorization(string url)
        {
            await _client.GetAsync(url);
        }

        public async Task<SessionDataModel> GetAccessSessionData()
        {
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
                throw new SessionDataParseException(ex.ToString());
            }
            return sessionData;
        }
    }
}