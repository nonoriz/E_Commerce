

using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace E_Commerce.Infrastructure.DataSeeding
{
    internal class CatalogDataSeeder(StoreDbContext dbContext , ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var PendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(ct);
                if (PendingMigrations.Any())
                    await dbContext.Database.MigrateAsync(ct);

                var SeedRoot = Path.Combine(AppContext.BaseDirectory, "DataSeed");

                await SeedIfEmptyAsync<ProductBrand, int>(SeedRoot, "brands.json", ct);
                await SeedIfEmptyAsync<ProductType, int>(SeedRoot, "types.json", ct);
                await SeedIfEmptyAsync<Product, int>(SeedRoot, "products.json", ct);
                await SeedIfEmptyAsync<DeliveryMethod, int>(SeedRoot, "delivery.json", ct);


                int Result = await dbContext.SaveChangesAsync(ct);
                if (Result > 0)
                    logger.LogInformation($"{Result} rows added");
                else
                    logger.LogInformation("Database already seeded");

            }
            catch
            {

            }
        }

        private async Task SeedIfEmptyAsync<T, TKey>(string rootPath, string fileName, CancellationToken ct) where T : BaseEntity<TKey>
        {
            if( await dbContext.Set<T>().AnyAsync())
            {
                logger.LogInformation("table already has data");
                return;
            }
             var filePath = Path.Combine(rootPath, fileName);
             
            if(!File.Exists(filePath))
            {
                logger.LogWarning($"Data seed file {fileName} not found at {filePath}");
                return;
            }

            using var fileStream=File.OpenRead(filePath);

            var options=new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive=true
            };

            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream, options, ct);
            if(items?.Any()?? false)
            {
                dbContext.Set<T>().AddRange(items);
            }




        }
    }
}
