using RouletteParser.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RouletteParser.GrandCasino.Exceptions;

namespace RouletteParser.GrandCasino
{
    public class Api
    {
        public HttpClient Client => _client;

        private readonly HttpClient _client;

        private readonly CookieContainer _container;

        private const string AuthCookieName = "auth_code";

        private const string GameCode = "evo_firstpersonroulettelobby";

        private const string GameId = "5448";

        private const string CurrencyId = "2";

        public Api()
        {
            HttpClientHandler handler = new HttpClientHandler();
            _container = new CookieContainer();
            _client = new HttpClient(handler.Configure(_container)).Configure();
        }

        /// <summary>
        /// This method get api code and write in cookie storage
        /// </summary>
        /// <param name="login">Login on site</param>
        /// <param name="password">Password on site</param>
        /// <returns></returns>
        public async Task Authorization(string login, string password)
        {
            var captchaAnswer = await RuCaptcha.Api.GetInstance().SolveRecaptchaV2(CaptchaSettings.SiteKey, Routes.Main);

            var data = new Dictionary<string, string>
            {
                {"form[method]", MethodsName.auth.ToString()},
                {"form[app]", ""},
                {"form[windows]", ""},
                {"form[username]", login},
                {"form[password]", password},
                {"g-recaptcha-response", captchaAnswer}

            };

            var answer = await (await _client.PostAsync(Routes.Api,
                new FormUrlEncodedContent(data))).Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject(answer) as JObject;

            if (string.IsNullOrEmpty(answer) || json?["code"] == null)
            {
                throw new ApiCodeParseException($"Was receive: {answer}");
            }

            _container.Add(new Cookie
            {
                Domain = Routes.Domain,
                Name = AuthCookieName,
                Value = json["code"].ToString()
            });
        }

        public async Task<string> GetLiveDealerUrl()
        {
            var data = new Dictionary<string, string>
            {
                {"form[method]", MethodsName.url.ToString()},
                {"form[game_code]", GameCode},
                {"form[game_id]", GameId},
                {"form[currency]", CurrencyId}

            };

            var answer = await (await _client.PostAsync(Routes.Api,
                new FormUrlEncodedContent(data))).Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject(answer) as JObject;

            if (string.IsNullOrEmpty(answer) || json?["url"] == null)
            {
                throw new UrlParseException($"Was receive: {answer}");
            }


            return json["url"].ToString();
        }
    }
}
