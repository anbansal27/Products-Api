using Microsoft.EntityFrameworkCore;
using Products.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Products.Api.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productContext;

        public ProductRepository(ProductDbContext productContext) =>
            _productContext = productContext ?? throw new ArgumentNullException(nameof(productContext));

        public async Task<int> AddAsync(Product product)
        {
            await _productContext.Products.AddAsync(product);
            var result = await SaveChangesAsync();
            return result;
        }

        public async Task<int> AddOptionAsync(ProductOption productOption)
        {
            await _productContext.ProductOptions.AddAsync(productOption);
            var result = await SaveChangesAsync();
            return result;
        }

        public async Task DeleteAsync(Product product)
        {
            _productContext.Products.Remove(product);
            await SaveChangesAsync();
        }

        public async Task DeleteOptionAsync(ProductOption productOption)
        {
            _productContext.ProductOptions.Remove(productOption);
            await SaveChangesAsync();
        }

        public async Task<Product> FirstOrDefaultAsync(Expression<Func<Product, bool>> predicate) =>
           await _productContext.Products
              .Where(predicate)
              .Include(x => x.ProductOptions)
              .FirstOrDefaultAsync();

        public async Task<List<Product>> GetProductsAsync(Expression<Func<Product, bool>> predicate) =>
           await _productContext.Products
              .AsNoTracking()
              .Where(predicate)
              .Include(x => x.ProductOptions)
              .ToListAsync();

        public async Task<int> SaveChangesAsync() =>
           await _productContext.SaveChangesAsync();

        public void Dispose()
        {
            _productContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
