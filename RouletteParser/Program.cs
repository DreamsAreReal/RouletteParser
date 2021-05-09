using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RouletteParser.GrandCasino;
using RouletteParser.RuCaptcha;
using Websocket.Client;

namespace RouletteParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            RuCaptcha.Api.GetInstance().SetToken(Settings.ApiKey);


            GrandCasino.Api grandCasinoApi = new GrandCasino.Api();
            await grandCasinoApi.Authorization("gsdgsg.guigui@bk.ru", "фиуиуя723");
            var url = await grandCasinoApi.GetLiveDealerUrl();
            LiveDealer.Api liveDealerApi = new LiveDealer.Api(grandCasinoApi.Client);
            await liveDealerApi.Authorization(url);
            var data = await liveDealerApi.GetAccessSessionData();

            return;

            
        }

        static string GetRandomPassword(string ch, int pwdLength)
        {
            Random rndGen = new Random();
            char[] pwd = new char[pwdLength];
            for (int i = 0; i < pwd.Length; i++)
                pwd[i] = ch[rndGen.Next(ch.Length)];
            return new string(pwd);
        }
    }
}
