using Microsoft.EntityFrameworkCore;
using WebShopApi.Database;
using WebShopApi.Models;

namespace WebShopApi.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products
                .Include(p => p.Items)
                .Include(p => p.Category)
                .Include(p => p.Brand).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByFilters(string search, IEnumerable<int> brandIds, IEnumerable<int> categoryIds, IEnumerable<string> sizes, IEnumerable<string> categoryNames)
        {
            IQueryable<Product> result = _dbContext.Products
                .Include(p => p.Items)
                .Include(p => p.Category)
                .Include(p => p.Brand);

            if (!String.IsNullOrEmpty(search))
            {
                result = result.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (brandIds != null && brandIds.Any())
            {
                result = result.Where(p => brandIds.Contains(p.Brand.Id));
            }

            if (categoryIds != null && categoryIds.Any())
            {
                result = result.Where(p => categoryIds.Contains(p.Category.Id));
            }
                 
            if (sizes != null && sizes.Any())
            {
                result = result.Where(p => p.Items.Any(pItem => sizes.Contains(pItem.Size)));
            }

            if (categoryNames != null && categoryNames.Any())
            {
                result = result.Where(p => categoryNames.Contains(p.Category.Name));
            }

            var products = await result.ToListAsync();

            var ratings = await _dbContext.Ratings
            .Include(r => r.OrderItem)
            .ThenInclude(o => o.Product)
            .Where(x => products.Select(p => p.Id).Contains(x.OrderItem.Product.Id))
            .ToListAsync();

            foreach (var product in products)
            {
                var productRatings = ratings.Where(r => r.OrderItem.Product.Id == product.Id);
                product.AvgRating = productRatings.Any() ?
                    (double)productRatings.Sum(p => p.Rate) / productRatings.Count()
                    : 0;
            }

            return products;
            
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product = await _dbContext.Products
                .Include(p => p.Items)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                return null;
            }

            var productRatings = await _dbContext.Ratings
                .Include(r => r.OrderItem)
                .ThenInclude(o => o.Product)
                .Where(r => product.Id == r.OrderItem.Product.Id)
                .ToListAsync();

            product.AvgRating = productRatings.Any() ?
                (double)productRatings.Sum(p => p.Rate) / productRatings.Count()
                : 0;

            return product;
        }

        public async Task<Product> AddProduct(Product product)
        {
            var result = await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductRating>> GetProductRatings(int productId)
        {
            return await _dbContext.Ratings
                .Include(r => r.Customer)
                .Include(r => r.OrderItem)
                .ThenInclude(o => o.Product)
                .Where(r => r.OrderItem.Product.Id == productId)
                .Select(r => new ProductRating
                {
                    ProductId = productId,
                    CustomerName = r.Customer.Name,
                    Rate = r.Rate,
                    CreationDate = r.OrderItem.Order.CreatedDate
                }).ToListAsync();
        }
    }
}
