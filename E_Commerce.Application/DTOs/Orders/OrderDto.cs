

using E_Commerce.Application.DTOs.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.DTOs.Orders
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; } = default!;
        [Required]

        public int DeliveryMethodId { get; set; }
        [Required]

        public AddressDto ShipToAddress { get; set; } = default!;
    }
}
