using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RouletteParser.Core;

namespace RouletteParser
{
    class Startup
    {
        private readonly ServiceProvider _serviceProvider;

        public Startup()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .Build();
            _serviceProvider = new ServiceCollection().Configure(config);
        }

        public async Task Start()
        {


                AbstractCasinoApi casinoApi = _serviceProvider.GetService<AbstractCasinoApi>();
                await casinoApi.Authorization("gsdgsg.guigui@bk.ru", "фиуиуя723");
                var url = await casinoApi.GetLiveDealerUrl();
                LiveDealer.Api liveDealerApi = _serviceProvider.GetService<LiveDealer.Api>();
                liveDealerApi.SetClient(casinoApi.Client);
                await liveDealerApi.Authorization(url);
                var data = await liveDealerApi.GetAccessSessionData();
                LiveDealer.WebsocketListener websocketListener =
                    _serviceProvider.GetService<LiveDealer.WebsocketListener>();
                await websocketListener.Receive(Log, data);
            


        }

        private void Log(string text)
        {
            Console.WriteLine(text);
        }
    }
}
