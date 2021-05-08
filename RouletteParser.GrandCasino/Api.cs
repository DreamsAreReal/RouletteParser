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
            _client = new HttpClient();


        }
    }
}
