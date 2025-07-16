using WebShopApi.Models;

namespace WebShopApi.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetOrdersForUser(Guid userId);
        Task<Order> GetOrderById(int orderId);
        Task<Order> AddOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(int orderId);
    }
}
