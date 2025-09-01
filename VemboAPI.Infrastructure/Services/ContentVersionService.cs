using VemboAPI.Infrastructure.Interfaces;

namespace VemboAPI.Infrastructure.Services;

public sealed class ContentVersionService : IContentVersionService
{
    private const string VersionKey = "content:version";
    private readonly ICacheService _cache;

    public ContentVersionService(ICacheService cache) => _cache = cache;

    public async Task<int> GetVersionAsync(CancellationToken ct = default)
    {
        var s = await _cache.GetStringAsync(VersionKey, ct);
        if (int.TryParse(s, out var v) && v > 0) return v;

        await _cache.SetStringAsync(VersionKey, "1", null, ct);
        return 1;
    }

    public async Task<int> BumpAsync(CancellationToken ct = default)
    {
        var cur = await GetVersionAsync(ct);
        var next = cur + 1;
        await _cache.SetStringAsync(VersionKey, next.ToString(), null, ct);
        return next;
    }
}
