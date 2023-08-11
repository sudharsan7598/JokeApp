using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Jokes.Common
{
	public class JokeHttpClientFactory : IJokeHttpClientFactory
	{
        private readonly IHttpClientFactory _client;
        private readonly ILogger<JokeHttpClientFactory> _logger;

        public JokeHttpClientFactory(IHttpClientFactory client, ILogger<JokeHttpClientFactory> logger)
        {
            _client = client;
            _logger = logger;
        }

        private HttpClient CreateClient(string uri, Dictionary<string, string> Headers = null)
        {
            var client = _client.CreateClient();

            client.Timeout = TimeSpan.FromSeconds(5);
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "MyApp");

            return client;
        }

        public async Task<Response> MakeGetCall<Response>(string path, string serviceURL, Dictionary<string, string> headers = null)
        {
            var response = default(Response);
            try
            {
                HttpClient restClient = CreateClient(serviceURL, headers);
                HttpResponseMessage result = await restClient.GetAsync(path);
                if (result.IsSuccessStatusCode)
                {
                    var responseAsString = await result.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<Response>(responseAsString);
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.HttpClientErrorMsg);
            }
            return response;
        }
    }
}

