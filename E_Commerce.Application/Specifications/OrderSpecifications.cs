using E_Commerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class OrderSpecifications:BaseSpecification<Order, Guid>
    {
        public OrderSpecifications(string email):base(x=>x.BuyerEmail == email)
        {

            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);
            AddOrderByDesc(x => x.OrderDate);
        }

        public OrderSpecifications(Guid Id,string email) : base(x => x.BuyerEmail == email && x.Id==Id)
        {

            AddInclude(x => x.DeliveryMethod);
            AddInclude(x => x.Items);
        }
    }
}
