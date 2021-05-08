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

        public Api(string token)
        {
            _solver = new TwoCaptcha.TwoCaptcha(token);
        }

        public async Task<string> SolveRecapthcaV2(string siteKey, string url)
        {
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
