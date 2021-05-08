using RouletteParser.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace RouletteParser.GrandCasino
{
    public class Api
    {
        public HttpClient Client => _client;

        private HttpClient _client;

        public Api()
        {
            HttpClientHandler handler = new HttpClientHandler();
            _client = new HttpClient(handler.Configure()).Configure();
        }


    }
}
