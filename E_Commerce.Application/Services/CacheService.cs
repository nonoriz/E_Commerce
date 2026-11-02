using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
           _cacheRepository = cacheRepository;
        }
        public async Task<string?> GetDataAsync(string cacheKey, CancellationToken ct = default)
        => await _cacheRepository.GetAsync(cacheKey);

        public async Task SetDataAsync(string cacheKey, object cacheValue, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            var jsonValue = JsonSerializer.Serialize(cacheValue, new JsonSerializerOptions()
            { 
                PropertyNamingPolicy=JsonNamingPolicy.CamelCase
            
            });

            await _cacheRepository.SetAsync(cacheKey,jsonValue, timeToLive, ct);
        }
    }
}
