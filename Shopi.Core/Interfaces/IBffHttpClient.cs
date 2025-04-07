namespace Shopi.Core.Interfaces;

public interface IBffHttpClient
{
    Task<HttpResponseMessage> PostJsonAsync<T>(Uri baseUrl, string url, T data);
    Task<HttpResponseMessage> PatchJsonAsync<T>(Uri baseUrl, string url, T data);

    Task<HttpResponseMessage> PostJsonAsyncWithoutData(Uri baseUrl, string url,
        Dictionary<string, string>? queryParams = null);

    Task<HttpResponseMessage> PatchAsyncWithoutData(Uri baseUrl, string url);

    Task<HttpResponseMessage> Get<T>(Uri baseUrl, string url,
        IDictionary<string, string>? queryParams = null, IDictionary<string, string>? headers = null);

    Task<HttpResponseMessage> Delete(Uri baseUrl, string url);
}