using AutoMapper;
using Products.Api.Application.Contracts;
using Products.Api.Entities;

namespace Products.Api.Application.MappingProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<ProductDto, Product>();
        }
    }
}
