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

     

        private const string ApiKey = "1577500a6b0e34f93319c74a90fd8a09";

        public Api()
        {
            _solver = new TwoCaptcha.TwoCaptcha(ApiKey);
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

            if (_solver == null)
            {
                throw new Exception("Set apikey for api");
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
