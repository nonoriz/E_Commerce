using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Specifications
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            if(spec.Criteria !=null)
                query=query.Where(spec.Criteria);

            if (spec.IncludeExpressions.Any())
                query = spec.IncludeExpressions.Aggregate(query, (current, nextExp) => current.Include(nextExp));

            if(spec.Orderby != null)
                query = query.OrderBy(spec.Orderby);
            else if (spec.OrderbyDescending != null)
                query= query.OrderByDescending(spec.OrderbyDescending);

            if(spec.IsPaginated)
                query= query.Skip(spec.Skip).Take(spec.Take);


                return query;
        }
    }
}
