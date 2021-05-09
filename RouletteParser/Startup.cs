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
    class Startup : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;
        private const string Login = "gsdgsg.guigui@bk.ru";
        private const string Password = "фиуиуя723";
        private static Startup _instance;

        public Startup()
        {
            _instance?.Dispose();
            _instance = this;

            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .Build();
            _serviceProvider = new ServiceCollection().Configure(config);
        }

        public async Task Start()
        {
            AbstractCasinoApi casinoApi = _serviceProvider.GetService<AbstractCasinoApi>();
            await casinoApi.Authorization(Login, Password);
            var url = await casinoApi.GetLiveDealerUrl();

            LiveDealer.Api liveDealerApi = _serviceProvider.GetService<LiveDealer.Api>();
            liveDealerApi?.SetClient(casinoApi.Client);
            await liveDealerApi.Authorization(url);
            var data = await liveDealerApi.GetAccessSessionData();
            
            LiveDealer.WebsocketListener websocketListener = 
                _serviceProvider.GetService<LiveDealer.WebsocketListener>();
            await websocketListener.Receive(_serviceProvider.GetService<Handler>().MessageReceived, data);
            


        }

        

        public void Dispose()
        {
            _serviceProvider.Dispose();
        }
    }
}
