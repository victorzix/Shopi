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
}