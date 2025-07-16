using Microsoft.EntityFrameworkCore;
using WebShopApi.Database;
using WebShopApi.Models;

namespace WebShopApi.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task AddCategory(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategory(int categoryId)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateCategory(Category category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
