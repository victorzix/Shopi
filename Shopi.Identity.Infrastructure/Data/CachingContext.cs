using StackExchange.Redis;

namespace Shopi.Identity.Infrastructure.Data;

public class CachingContext
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public CachingContext(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public async Task<string> GetValue(string key)
    {
        var redisDb = _connectionMultiplexer.GetDatabase();
        return await redisDb.StringGetAsync(key);
    }

    public async Task SetValue(string key, string value, TimeSpan ttl)
    {
        var redisDb = _connectionMultiplexer.GetDatabase();
        await redisDb.StringSetAsync(key, value, ttl);
    }
}