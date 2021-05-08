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
            Api ruCaptcha = new Api(Settings.ApiKey);
            var a = await ruCaptcha.SolveRecapthcaV2("6Ldt8sgZAAAAAHJFb0N5Zmgy2ChjZGufVPpdADgj", "https://grand-casino-online.live/ru/");


            CookieContainer container = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseCookies = true;
            handler.CookieContainer = container;
            handler.AllowAutoRedirect = true;

            HttpClient client = new HttpClient(handler);
            var data = new Dictionary<string, string>
            {
                {"form[method]", "auth"},
                {"form[app]", ""},
                {"form[windows]", ""},
                {"form[username]", "login"},
                {"form[password]", "pass"},
                {"g-recaptcha-response", a}

            };

            var b = await (await client.PostAsync("https://grand-casino-online.live/ru/api",
                new FormUrlEncodedContent(data))).Content.ReadAsStringAsync();
            var code = (JsonConvert.DeserializeObject(b) as JObject)["code"];

            container.Add(new Cookie
            {
                Domain = "grand-casino-online.live",
                Name = "auth_code",
                Value = code.ToString()
            });

            var data1 = new Dictionary<string, string>
            {
                {"form[method]", "url"},
                {"form[game_code]", "evo_firstpersonroulettelobby"},
                {"form[game_id]", "5448"},
                {"form[currency]", "2"}

            };

            var c = await (await client.PostAsync("https://grand-casino-online.live/ru/api",
                new FormUrlEncodedContent(data1))).Content.ReadAsStringAsync();

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
            var gggds = GetRandomPassword("1234567890QWERTYUIOPPASDFGHJKLZXCVBNMqwertyuuiopasdfghjklkzxcvbnm", 5);
            var url = new Uri("wss://livedealer5.fh8labs.com/public/roulette/player/game/vctlz20yfnmp1ylr/socket?messageFormat=json&instance="+GetRandomPassword("1234567890QWERTYUIOPPASDFGHJKLZXCVBNMqwertyuuiopasdfghjklkzxcvbnm", 5)+"-pfgb2txpbmvpvuwa-vctlz20yfnmp1ylr&tableConfig=&EVOSESSIONID="+ session + "&client_version=6.20210506.71858.6016-243dac1b2c");
            var exitEvent = new ManualResetEvent(false);

            using (var client1 = new WebsocketClient(url))
            {
                client1.MessageReceived.Subscribe(msg =>
                {
                    var outMsg = JsonConvert.DeserializeObject(msg.Text) as JObject;
                    if (outMsg?["type"]?.ToString()== "roulette.winSpots")
                    {
                        Console.WriteLine(msg);
                    }
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
