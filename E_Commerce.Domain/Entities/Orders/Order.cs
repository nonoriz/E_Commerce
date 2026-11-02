using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.Orders
{
    public class Order:BaseEntity<Guid>
    {
        private Order()
        {
            
        }
        public Order(string buyerEmail, OrderAddress shipToAddress, ICollection<OrderItem> items, DeliveryMethod deliveryMethod, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            Items = items;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string PaymentIntentId { get; set; } 

        public string BuyerEmail { get; set; } = default!;
        

        public OrderAddress ShipToAddress { get; set; } = default!;

        public ICollection<OrderItem> Items { get; set; } = [];

        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public decimal SubTotal { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public int DeliveryMethodId { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public decimal GetTotal() => SubTotal + (DeliveryMethod?.Cost ?? 0);

    }
}
