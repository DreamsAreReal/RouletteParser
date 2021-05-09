using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using RouletteParser.Core;


namespace RouletteParser
{
    static class ServiceCollectionExtensions
    {
        public static ServiceProvider Configure(this ServiceCollection collection, IConfigurationRoot config)
        {
            return new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                    loggingBuilder.AddNLog(config);
                })
                .AddSingleton<RuCaptcha.Api>()
                .AddTransient<AbstractCasinoApi, GrandCasino.Api>()
                .AddTransient<LiveDealer.Api>()
                .AddTransient<LiveDealer.WebsocketListener>()
                .BuildServiceProvider();
        }
    }
}
