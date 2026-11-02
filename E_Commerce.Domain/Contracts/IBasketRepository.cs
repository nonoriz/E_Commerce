

using E_Commerce.Domain.Entities.Baskets;

namespace E_Commerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct = default!);

        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive=null, CancellationToken ct = default!);

        Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default!);
    }
}
