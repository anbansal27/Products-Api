using Products.Api.Application.Response;
using System.Threading.Tasks;
using Products.Api.Contracts;

namespace Products.Api.Application.Services
{
    public interface IProductService
    {
        Task<CreateProductResponse> CreateProduct(ProductDto productDto);
    }
}
