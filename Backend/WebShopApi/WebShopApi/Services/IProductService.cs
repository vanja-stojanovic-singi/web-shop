using WebShopApi.Models;

namespace WebShopApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByFilters(string search, IEnumerable<int> brandIds, IEnumerable<int> categoryIds, IEnumerable<string> sizes, IEnumerable<string> categoryNames);
        Task<Product> GetProductById(int productId);
        Task<Product> AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int productId);
        Task<ProductFilterOptions> GetProductFilterOptions();
        Task<IEnumerable<ProductRating>> GetProductRatings(int productId);
    }
}
