using E_Commerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    internal class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;
        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();


        }
        public async Task<string?> GetAsync(string cacheKey, CancellationToken ct = default)
        {
            var Value =await _database.StringGetAsync(cacheKey);
            return Value.IsNullOrEmpty ? null : Value.ToString();
        }

        public async Task SetAsync(string caceKey, string cacheValue, TimeSpan? timeToLive = default, CancellationToken ct = default)
        {
            await _database.StringSetAsync(caceKey, cacheValue, timeToLive ?? TimeSpan.FromDays(2));
        }
    }
}
