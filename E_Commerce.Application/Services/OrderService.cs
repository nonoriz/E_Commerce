using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _basketRepository = basketRepository;
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email, CancellationToken ct = default)
        {
            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId, ct);

            if (basket == null)
                return Error.NotFound("Basket Not Found", $"Basket with id {orderDto.BasketId} is Not found");
            if (basket.Items.Count == 0)
                return Error.Validation("Basket Is Empty", $"Can Not Create Order With Basket Id {basket.Id}");

            var exOrder = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new PaymentIntentSpec(basket.PaymentIntentId));

            if (exOrder != null)
                _unitOfWork.GetRepository<Order, Guid>().Remove(exOrder);



            var ordeItems = new List<OrderItem>(basket.Items.Count);
            var productIds = basket.Items.Select(x => x.Id).ToHashSet();
            var products = (await _unitOfWork.GetRepository<Product, int>()
                .GetAllAsync(new ProductWithIdSpecifications(productIds), ct)).ToDictionary(x => x.Id);
            foreach(var item in basket.Items)
            {
                if (!products.TryGetValue(item.Id, out var product))
                    return Error.NotFound("Product Not Fiund", $"Product With Id {item.Id} Is Not Found");
                ordeItems.Add(new OrderItem()
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductItemOrdered()
                    {
                        PictureUrl = product.PictureUrl,
                        ProductId = product.Id,
                        ProductName = product.Name
                    }
                });
            }

            var orderAddress = _mapper.Map<OrderAddress>(orderDto.ShipToAddress);

            var deliverMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);

            if (deliverMethod == null)
                return Error.NotFound("Delivery Method Not Found", $"Delivery Method With Id {orderDto.DeliveryMethodId} Is Not Found");

            var subTotal = ordeItems.Sum(x => x.Quantity * x.Price);

            var order = new Order(email, orderAddress, ordeItems, deliverMethod, subTotal,basket.PaymentIntentId);

            _unitOfWork.GetRepository<Order,Guid>().Add(order);
            var result =await _unitOfWork.SaveChangesAsync(ct);
            if (result == 0)
                return Error.Failure("Order Save Failed", "Can Not Create Order");
            else
            {
                await _basketRepository.DeleteBasketAsync(orderDto.BasketId, ct);
                return _mapper.Map<OrderToReturnDto>(order);
            }

        }

        public async Task<Result<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersForUserAsync(string email, CancellationToken ct = default)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderSpecifications(email), ct);
            if (orders.Any())
                return Result<IReadOnlyList<OrderToReturnDto>>.Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
            else
                return Error.NotFound("Order Not Found", $"No Orders Found For User With Email {email}");
        }

        public async Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethodsAsync(CancellationToken ct = default)
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync(ct);
            if (deliveryMethods.Any())
                return Result<IReadOnlyList<DeliveryMethodDto>>.Ok(_mapper.Map<IReadOnlyList<DeliveryMethodDto>>(deliveryMethods));
            else
                return Error.NotFound("No Delivery Methods Found");
        }

        public async Task<Result<OrderToReturnDto>> GetOrderByIdAndEmailAsync(Guid id, string email, CancellationToken ct = default)
        {
            var order = await _unitOfWork.GetRepository<Order,Guid>().GetByIdAsync(new OrderSpecifications(id,email), ct);
            if (order == null)
                return Error.NotFound("Order Not Found", $"Order With Id {id} Was Not Found");
            else
                return _mapper.Map<OrderToReturnDto>(order);
        }
    }
}
