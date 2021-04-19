using AutoMapper;
using Products.Api.Application.Contracts;
using Products.Api.Entities;

namespace Products.Api.Application.MappingProfile
{
    public class ProductOptionProfile : Profile
    {
        public ProductOptionProfile()
        {
            CreateMap<ProductOptionDto, ProductOption>();

            CreateMap<ProductOption, ProductOptionDto>();
        }
    }
}
