using AutoMapper;
using E_Commerce.Application.DTOs.Identity;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profiles
{
    internal class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(x => x.DeliveryMethod, o => o.MapFrom(x => x.DeliveryMethod.ShortName))
                .ForMember(x => x.DeliveryMethodCost, o => o.MapFrom(x => x.DeliveryMethod.Cost));


            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(x => x.ProductId, o => o.MapFrom(x => x.Product.ProductId))
                .ForMember(x => x.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>())
                .ForMember(x => x.ProductName, o => o.MapFrom(x => x.Product.ProductName));

            CreateMap<DeliveryMethod, DeliveryMethodDto>();



        }
    }
}
