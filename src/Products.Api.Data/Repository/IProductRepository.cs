using Products.Api.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Products.Api.Data.Repository
{
    public interface IProductRepository
    {
        Task<int> AddAsync(Product product);

        Task Delete(Product product);

        Task<Product> FirstOrDefaultAsync(Expression<Func<Product, bool>> predicate);

        Task<int> SaveChangesAsync();        
    }
}
