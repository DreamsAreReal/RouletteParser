using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace RouletteParser.Core
{
    public static class HttpHandlerExtensions
    {
        public static HttpClientHandler Configure(this HttpClientHandler handler)
        {
            handler.UseCookies = true;
            handler.CookieContainer = new CookieContainer();
            handler.AllowAutoRedirect = true;
            return handler;
        }
    }
}
