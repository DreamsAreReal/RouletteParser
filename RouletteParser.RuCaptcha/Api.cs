using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RouletteParser.RuCaptcha
{
    public class Api
    {
        private TwoCaptcha.TwoCaptcha _solver;

        public Api(string token)
        {
            TwoCaptcha.TwoCaptcha solver = new TwoCaptcha.TwoCaptcha(token);
        }

        public async Task<double> GetBalance()
        {
            return await _solver.Balance();
        }
    }
}
