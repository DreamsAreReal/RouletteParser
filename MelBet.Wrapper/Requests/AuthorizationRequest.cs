using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace RoulletteParser.MelBet.Wrapper.Requests
{
    internal class AuthorizationRequest
    {
        public void Make(HttpClient client, string login, string password)
        {
            var loginEncode = Convert.ToBase64String(login.);


            var answer = await (await client.PostAsync())
        }
    }
}
