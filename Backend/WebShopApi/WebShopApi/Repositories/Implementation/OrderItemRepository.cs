using Microsoft.EntityFrameworkCore;
using WebShopApi.Database;
using WebShopApi.Models;

namespace WebShopApi.Repositories.Implementation
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItems()
        {
            return await _dbContext.OrderItems.ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemById(int orderItemId)
        {
            return await _dbContext.OrderItems.FirstOrDefaultAsync(o => o.Id == orderItemId);
        }

        public async Task AddOrderItem(OrderItem orderItem)
        {
            await _dbContext.OrderItems.AddAsync(orderItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderItem(int orderItemId)
        {
            var orderItem = await _dbContext.OrderItems.FirstOrDefaultAsync(o => o.Id == orderItemId);

            if (orderItem != null)
            {
                _dbContext.OrderItems.Remove(orderItem);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateOrderItem(OrderItem orderItem)
        {
            _dbContext.OrderItems.Update(orderItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
