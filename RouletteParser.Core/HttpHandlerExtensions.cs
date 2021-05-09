using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace RouletteParser.Core
{
    public static class HttpHandlerExtensions
    {
        public static HttpClientHandler Configure(this HttpClientHandler handler, CookieContainer container)
        {
            handler.UseCookies = true;
            handler.CookieContainer = container;
            handler.AllowAutoRedirect = true;
            return handler;
        }
    }
}
