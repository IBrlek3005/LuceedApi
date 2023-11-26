using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Luceed_API.Services
{
    public abstract class HttpService
    {
        protected readonly IHttpClientFactory _clientFactory;

        protected HttpService (IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        protected virtual async Task<TResponse> ExecuteGetWithResponse<TResponse>(string clientName, string endpoint, string credentials)
        {
            var client = _clientFactory.CreateClient(clientName);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            var response = await client.GetAsync(endpoint);
            return await GetResponseAsync<TResponse>(response);
        }

        protected virtual async Task<T> GetResponseAsync<T>(HttpResponseMessage response)
        {
            var message = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(message);
            }

            return default;
        }
    }
}
