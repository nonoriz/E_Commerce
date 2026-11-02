using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class ProductWithIdSpecifications : BaseSpecification<Product,int>
    {
        public ProductWithIdSpecifications(HashSet<int> ProductIds) : base(p => ProductIds.Contains(p.Id))
        {
            
        }
    }
}
