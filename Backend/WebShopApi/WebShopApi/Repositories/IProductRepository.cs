using WebShopApi.Models;

namespace WebShopApi.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByFilters(string search, IEnumerable<int> brandIds, IEnumerable<int> categoryIds, IEnumerable<string> sizes, IEnumerable<string> categoryNames);
        Task<Product> GetProductById(int productId);
        Task<Product> AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int productId);
        Task<IEnumerable<ProductRating>> GetProductRatings(int productId);
    }
}
