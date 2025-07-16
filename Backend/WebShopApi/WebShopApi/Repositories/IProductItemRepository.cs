using WebShopApi.Models;

namespace WebShopApi.Repositories
{
    public interface IProductItemRepository
    {
        Task<IEnumerable<ProductItem>> GetProductItems();
        Task<ProductItem> GetProductItemById(int productItemId);
        Task AddProductItem(ProductItem productItem);
        Task UpdateProductItem(ProductItem productItem);
        Task DeleteProductItem(int productId, string size);
    }
}
