using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RouletteParser.GrandCasino;
using RouletteParser.LiveDealer;
using RouletteParser.RuCaptcha;
using Websocket.Client;

namespace RouletteParser
{
    class Program
    {
        static async Task Main(string[] args)
        {

            await new Startup().Start();

            
            await Task.Run(()  =>
            {
                Console.WriteLine("Enter \"exit\" for exit");
                while (true)
                {
                    var input = Console.ReadLine();
                    if (input == "exit")
                    {
                        break;
                    }
                }
            });



        }

        

        
    }
}
