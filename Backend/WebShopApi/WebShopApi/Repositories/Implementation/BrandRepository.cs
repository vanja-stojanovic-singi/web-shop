using Microsoft.EntityFrameworkCore;
using WebShopApi.Database;
using WebShopApi.Models;

namespace WebShopApi.Repositories.Implementation
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _dbContext;

        public BrandRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Brand>> GetBrands()
        {
            return await _dbContext.Brands.ToListAsync();
        }

        public async Task<Brand> GetBrandById(int brandId)
        {
            return await _dbContext.Brands.FirstOrDefaultAsync(b => b.Id == brandId);
        }

        public async Task AddBrand(Brand brand)
        {
            await _dbContext.Brands.AddAsync(brand);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBrand(int brandId)
        {
            var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Id == brandId);

            if (brand != null)
            {
                _dbContext.Brands.Remove(brand);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateBrand(Brand brand)
        {
            _dbContext.Brands.Update(brand);
            await _dbContext.SaveChangesAsync();
        }
    }
}
