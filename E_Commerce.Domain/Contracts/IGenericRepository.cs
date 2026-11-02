
using E_Commerce.Domain.Common;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity ,TKey> where TEntity :BaseEntity<TKey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);

        Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity,TKey> Spec, CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);

        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> Spec,CancellationToken ct = default);

        Task<int> CountAsync(ISpecifications<TEntity, TKey> spec, CancellationToken ct = default!);


    }
}
