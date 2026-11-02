using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto, CancellationToken ct = default)
            => ToActionResult(await _orderService.CreateOrderAsync(orderDto, GetEmailFromToken(), ct));
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrders(CancellationToken ct= default)
            => ToActionResult(await _orderService.GetAllOrdersForUserAsync(GetEmailFromToken(), ct));

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id, CancellationToken ct = default)
            =>ToActionResult(await _orderService.GetOrderByIdAndEmailAsync(id, GetEmailFromToken(), ct));

        [AllowAnonymous]
        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetDelivryMethods(CancellationToken ct)
            => ToActionResult(await _orderService.GetDeliveryMethodsAsync(ct));

    }
}
