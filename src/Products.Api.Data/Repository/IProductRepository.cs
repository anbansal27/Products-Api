using Products.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Products.Api.Data.Repository
{
    public interface IProductRepository
    {
        Task<int> AddAsync(Product product);

        Task<int> AddOptionAsync(ProductOption productOption);

        Task DeleteAsync(Product product);

        Task DeleteOptionAsync(ProductOption productOption);

        Task<Product> FirstOrDefaultAsync(Expression<Func<Product, bool>> predicate);

        Task<List<Product>> GetProductsAsync(Expression<Func<Product, bool>> predicate);

        Task<int> SaveChangesAsync();
    }
}
