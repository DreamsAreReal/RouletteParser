using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using RandomUserAgent;

namespace RouletteParser.Core
{
    public static class HttpClientExtensions
    {
        private const string NameUserAgentHeader = "user-agent";
        

        public static HttpClient Configure(this HttpClient client)
        {
            client.DefaultRequestHeaders.Add(NameUserAgentHeader, RandomUa.RandomUserAgent);
            return client;
        }


    }
}
