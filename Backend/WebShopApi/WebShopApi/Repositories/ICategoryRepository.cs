using WebShopApi.Models;

namespace WebShopApi.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int categoryId);
    }
}
