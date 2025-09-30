namespace VemboAPI.Infrastructure.Interfaces
{
    public interface IContentVersionService
    {
        Task<int> GetVersionAsync(CancellationToken ct = default);
        Task<int> BumpAsync(CancellationToken ct = default);
    }
}