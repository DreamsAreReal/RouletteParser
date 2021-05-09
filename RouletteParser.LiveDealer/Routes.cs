using System;
using System.Collections.Generic;
using System.Text;

namespace RouletteParser.LiveDealer
{
    internal class Routes
    {
        public const string SetupDevice = "https://livedealer5.fh8labs.com/setup?device=desktop&wrapped=false";

        public const string GameId = "vctlz20yfnmp1ylr";
        public const string ClientId = "6.20210506.71858.6016-243dac1b2c";

        public const string WebsocketUrl =
            "wss://livedealer5.fh8labs.com/public/roulette/player/game/"+GameId+"/socket?messageFormat=json";
    }
}
