using Microsoft.EntityFrameworkCore;
using WebShopApi.Database;
using WebShopApi.Models;

namespace WebShopApi.Repositories.Implementation
{
    public class ProductItemRepository : IProductItemRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductItem>> GetProductItems()
        {
            return await _dbContext.ProductItems.ToListAsync();
        }

        public async Task<ProductItem> GetProductItemById(int productItemId)
        {
            return await _dbContext.ProductItems.FirstOrDefaultAsync(p => p.Id == productItemId);
        }

        public async Task AddProductItem(ProductItem productItem)
        {
            await _dbContext.ProductItems.AddAsync(productItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductItem(int productId, string size)
        {
            var productItem = await _dbContext.ProductItems.FirstOrDefaultAsync(b => b.Product.Id == productId && b.Size == size);

            if (productItem != null)
            {
                _dbContext.ProductItems.Remove(productItem);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateProductItem(ProductItem productItem)
        {
            _dbContext.ProductItems.Update(productItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
