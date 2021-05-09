using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RouletteParser.Core
{
    public abstract class AbstractCasinoApi
    {
        public HttpClient Client => _client;
        protected HttpClient _client;
        public abstract Task Authorization(string login, string password);
        public abstract Task<string> GetLiveDealerUrl();
    }
}
