using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data;


namespace E_Commerce.Infrastructure.Repositories
{
    internal class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var TypeName = typeof(TEntity).Name;
            if(repositories.TryGetValue(TypeName, out object? value))
                return (IGenericRepository<TEntity, TKey>)value;
            else
            {
                var repo=new GenericRepository<TEntity,TKey>(dbContext);
                repositories[TypeName] = repo;
                return repo;
            }
           
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)=>await dbContext.SaveChangesAsync(ct);
        
    }
}
