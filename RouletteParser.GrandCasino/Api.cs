using RouletteParser.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RouletteParser.GrandCasino.Exceptions;

namespace RouletteParser.GrandCasino
{

    public class Api : AbstractCasinoApi
    {
       

        private readonly CookieContainer _container;

        private const string AuthCookieName = "auth_code";

        private const string GameCode = "evo_firstpersonroulettelobby";

        private const string GameId = "5448";

        private const string CurrencyId = "2";

        private readonly RuCaptcha.Api _ruCaptchaApi;

        private readonly ILogger<CallConvThiscall> _logger;

        public Api(RuCaptcha.Api ruCaptchaApi, ILogger<CallConvThiscall> logger)
        {
            _logger = logger;
            _ruCaptchaApi = ruCaptchaApi;
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
        public override async Task Authorization(string login, string password)
        {

            _logger.LogDebug("Get captcha from recaptcha");
            var captchaAnswer = await _ruCaptchaApi.SolveRecaptchaV2(CaptchaSettings.SiteKey, Routes.Main);
            _logger.LogDebug("Captcha answer get");
            _logger.LogDebug($"Try authorization on {Routes.Main}");
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
                _logger.LogCritical($"Authorization on grand casino was receive: {answer}");
                throw new ApiCodeParseException($"Was receive: {answer}");
            }
            _logger.LogDebug($"Authorization on grand casino is success");
            _container.Add(new Cookie
            {
                Domain = Routes.Domain,
                Name = AuthCookieName,
                Value = json["code"].ToString()
            });
        }

        /// <summary>
        /// Method get url for authorization on live dealer
        /// </summary>
        /// <returns>Url for authorization</returns>
        public override async Task<string> GetLiveDealerUrl()
        {
            var data = new Dictionary<string, string>
            {
                {"form[method]", MethodsName.url.ToString()},
                {"form[game_code]", GameCode},
                {"form[game_id]", GameId},
                {"form[currency]", CurrencyId}

            };
            _logger.LogDebug($"Get url for authorization on live dealer");
            var answer = await (await _client.PostAsync(Routes.Api,
                new FormUrlEncodedContent(data))).Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject(answer) as JObject;

            if (string.IsNullOrEmpty(answer) || json?["url"] == null)
            {
                _logger.LogCritical($"Getting url for authorization on live dealer was receive {answer}");
                throw new UrlParseException($"Was receive: {answer}");
            }

            _logger.LogDebug($"Url for authorization on live dealer was receive");
            return json["url"].ToString();
        }
    }
}
