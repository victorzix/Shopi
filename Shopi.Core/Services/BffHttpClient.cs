using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Shopi.Core.Interfaces;

namespace Shopi.Core.Services;

public class BffHttpClient : IBffHttpClient
{
    private readonly HttpClient _httpClient;

    public BffHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> PostJsonAsync<T>(Uri baseUrl, string url, T data)
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var requestUri = new Uri(baseUrl, url);

        return await _httpClient.PostAsync(requestUri, content);
    }

    public async Task<HttpResponseMessage> PatchJsonAsync<T>(Uri baseUrl, string url, T data)
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var requestUri = new Uri(baseUrl, url);

        return await _httpClient.PatchAsync(requestUri, content);
    }

    public async Task<HttpResponseMessage> Get<T>(Uri baseUrl, string url,
        IDictionary<string, string>? queryParams = null, IDictionary<string, string>? headers = null)
    {
        var requestUri = new Uri(baseUrl, url);

        if (queryParams != null && queryParams.Any())
        {
            var queryString = string.Join("&", queryParams.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));
            requestUri = new Uri($"{requestUri}?{queryString}");
        }

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }
        
        return await _httpClient.SendAsync(request);
    }
}