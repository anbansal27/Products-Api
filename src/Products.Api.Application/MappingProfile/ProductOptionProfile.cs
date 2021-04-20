using AutoMapper;
using Products.Api.Application.Dto;
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
