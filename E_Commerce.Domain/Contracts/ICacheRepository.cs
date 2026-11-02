

namespace E_Commerce.Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string cacheKey, CancellationToken ct = default!);

        Task SetAsync(string caceKey, string cacheValue, TimeSpan? timeToLive=default,  CancellationToken ct = default!);

    }
}
