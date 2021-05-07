using System.Net.Http;

namespace RoulletteParser.MelBet.Wrapper
{
    public class Api
    {
        private HttpClient _httpClient;

        public Api()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            _httpClient = new HttpClient(httpClientHandler);
        }

        public void Authorization(string login, string password)
        {

        }
    }
}
