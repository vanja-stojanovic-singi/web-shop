using WebShopApi.Models;

namespace WebShopApi.Repositories
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetOrderItems();
        Task<OrderItem> GetOrderItemById(int orderItemId);
        Task AddOrderItem(OrderItem orderItem);
        Task UpdateOrderItem(OrderItem orderItem);
        Task DeleteOrderItem(int orderItemId);
    }
}
