namespace Shopi.Core.Utils;

public class ApiResponses<T>
{
    public T? Data { get; set; }
    public IEnumerable<string>? Errors { get; set; }
    public bool Success { get; set; }
    public int? StatusCode { get; set; }
}