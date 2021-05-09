using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RouletteParser.RuCaptcha;
using Websocket.Client;

namespace RouletteParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            RuCaptcha.Api.GetInstance().SetToken(Settings.ApiKey);


            var gg = await (await client.GetAsync((JsonConvert.DeserializeObject(c) as JObject)["url"].ToString())).Content
                .ReadAsStringAsync();

            var aaa =
                await (await client.GetAsync("https://livedealer5.fh8labs.com/setup?device=desktop&wrapped=false"))
                    .Content.ReadAsStringAsync();

            var cookies = new List<Cookie>();
            var cookies3 = new List<Cookie>();
            foreach (Cookie cookie in container.GetCookies(new Uri("https://grand-casino-online.live")))
            {
                cookies.Add(cookie);
            }
            foreach (Cookie cookie in container.GetCookies(new Uri("https://livedealer5.fh8labs.com")))
            {
                cookies3.Add(cookie);
            }

            var session = (JsonConvert.DeserializeObject(aaa) as JObject)["session_id"].ToString();
            var url = new Uri("wss://livedealer5.fh8labs.com/public/roulette/player/game/vctlz20yfnmp1ylr/socket?messageFormat=json&instance="+GetRandomPassword("1234567890QWERTYUIOPPASDFGHJKLZXCVBNMqwertyuuiopasdfghjklkzxcvbnm", 5)+"-pfgb2txpbmvpvuwa-vctlz20yfnmp1ylr&tableConfig=&EVOSESSIONID="+ session + "&client_version=6.20210506.71858.6016-243dac1b2c");
            var exitEvent = new ManualResetEvent(false);

            using (var client1 = new WebsocketClient(url))
            {
                client1.MessageReceived.Subscribe(msg =>
                {
                    
                        Console.WriteLine(msg);
                    
                });
                await client1.Start();


                exitEvent.WaitOne();
            }

            Console.ReadLine();
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
