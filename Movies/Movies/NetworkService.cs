using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Movies
{
    internal class NetworkService : INetworkService
    {
        private HttpClient _httpClient;

        public NetworkService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<TResult> GetTask<TResult>(String uri)
        {
            HttpResponseMessage resposnse = await _httpClient.GetAsync(uri);
            String serialized = await resposnse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResult>(serialized);
            return result;
        }
    }
}
