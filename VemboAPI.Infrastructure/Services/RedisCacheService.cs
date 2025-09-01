using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services;

public sealed class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _json;

    public RedisCacheService(IDistributedCache cache, IOptions<JsonSerializerOptions> jsonOptions)
    {
        _cache = cache;
        _json = jsonOptions.Value;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
    {
        var s = await _cache.GetStringAsync(key, ct);
        return s is null ? default : JsonSerializer.Deserialize<T>(s, _json);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        var s = JsonSerializer.Serialize(value, _json);
        var opt = new DistributedCacheEntryOptions();
        if (ttl.HasValue)
            opt.AbsoluteExpirationRelativeToNow = ttl;
        await _cache.SetStringAsync(key, s, opt, ct);
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        var hit = await GetAsync<T>(key, ct);
        if (hit is not null)
            return hit;

        var val = await factory();
        await SetAsync(key, val, ttl, ct);
        return val;
    }

    public Task RemoveAsync(string key, CancellationToken ct = default)
        => _cache.RemoveAsync(key, ct);

    public Task<string?> GetStringAsync(string key, CancellationToken ct = default)
        => _cache.GetStringAsync(key, ct);

    public Task SetStringAsync(string key, string value, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        var opt = new DistributedCacheEntryOptions();
        if (ttl.HasValue)
            opt.AbsoluteExpirationRelativeToNow = ttl;
        return _cache.SetStringAsync(key, value, opt, ct);
    }
}
