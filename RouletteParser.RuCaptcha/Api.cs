using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwoCaptcha.Captcha;

namespace RouletteParser.RuCaptcha
{
    public class Api
    {
        private readonly TwoCaptcha.TwoCaptcha _solver;

        private static Api _instance;

        private string _token;

        public static Api GetInstance()
        {
            if (_instance != null)
            {
                _instance = new Api();
            }

            return _instance;
        }

        /// <summary>
        /// Set rucaptcha token
        /// </summary>
        /// <param name="token">Token for API</param>
        public void SetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException($"Token not must be null");
            }
            _token = token;
        }

        

        /// <summary>
        /// This method solve recapcha v2.
        /// </summary>
        /// <param name="siteKey">Sitekey from site. Is const</param>
        /// <param name="url">Url from site, where locate captcha</param>
        /// <returns>Answer for captcha</returns>
        public async Task<string> SolveRecaptchaV2(string siteKey, string url)
        {
            if (string.IsNullOrEmpty(siteKey))
            {
                throw new ArgumentException("Sitekey not must be null");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Url not must be null");
            }

            ReCaptcha captcha = new ReCaptcha();
            captcha.SetSiteKey(siteKey);
            captcha.SetUrl(url);
            captcha.SetInvisible(true);
            captcha.SetAction("verify");

            await _solver.Solve(captcha);
            return captcha.Code;
        }
    }
}
