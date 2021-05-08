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
        public static HttpClientHandler ConfigureHandler(this HttpClientHandler handler)
        {
            handler.UseCookies = true;
            handler.CookieContainer = new CookieContainer();
            handler.AllowAutoRedirect = true;
            return handler;
        }

        public static HttpClient ConfigureClient(this HttpClient client)
        {
            client.DefaultRequestHeaders.Add(NameUserAgentHeader, RandomUa.RandomUserAgent);
            return client;
        }


    }
}
