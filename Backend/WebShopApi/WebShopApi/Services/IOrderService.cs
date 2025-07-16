using WebShopApi.Models;

namespace WebShopApi.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetOrdersByUser(Guid userId);
        Task<Order> GetOrderById(int orderId);
        Task<Order> AddOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(int orderId);
        Task RateOrder(int orderItemId, Guid userId, int rate);
    }
}
