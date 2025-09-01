using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VemboAPI.Infrastructure.Interfaces
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken ct = default);
        Task SetAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default);
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? ttl = null, CancellationToken ct = default);
        Task RemoveAsync(string key, CancellationToken ct = default);

        Task<string?> GetStringAsync(string key, CancellationToken ct = default);
        Task SetStringAsync(string key, string value, TimeSpan? ttl = null, CancellationToken ct = default);
    }
}