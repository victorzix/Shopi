namespace Shopi.Core.Interfaces;

public interface IBffHttpClient
{
    Task<HttpResponseMessage> PostJsonAsync<T>(Uri baseUrl, string url, T data);
    Task<HttpResponseMessage> PatchJsonAsync<T>(Uri baseUrl, string url, T data);
}