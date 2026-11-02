

using AutoMapper;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Profiles
{
    internal class ProducProfile:Profile
    {
        public ProducProfile()
        {
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType,TypeDto>();
            CreateMap<Product, ProductDto>()
                .ForMember(dst => dst.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dst => dst.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dst=>dst.PictureUrl,opt=>opt.MapFrom<PictureUrlResolver>());
        }

    }
}
