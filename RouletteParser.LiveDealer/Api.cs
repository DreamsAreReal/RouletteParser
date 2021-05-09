using System.Net.Http;

namespace RouletteParser.LiveDealer
{
    public class Api
    {
        private HttpClient _client;

        public Api(HttpClient client)
        {
            _client = client;
        }

        public void Authorization(string url)
        {

        }
    }
}