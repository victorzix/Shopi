namespace Shopi.Core.Utils;

public static class QueryParamsUtils
{
    public static Dictionary<string, string> ToDictionary(this object obj)
    {
        return obj.GetType()
            .GetProperties()
            .Where(p => p.GetValue(obj) != null)
            .ToDictionary(
                p => p.Name,
                p => p.GetValue(obj)?.ToString() ?? string.Empty
            );
    }
}