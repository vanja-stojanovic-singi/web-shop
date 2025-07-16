using Microsoft.EntityFrameworkCore;
using WebShopApi.Database;
using WebShopApi.Models;
using System.Text.Json;

namespace WebShopApi.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersForUser(Guid userId)
        {
            var orders = await _dbContext.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Brand)
                .Where(o => o.Customer.Id == userId).ToListAsync();

            var orderItemIds = new List<int>();

            foreach (var order in orders)
            {
                orderItemIds = orderItemIds.Concat(order.Items.Select(i => i.Id)).ToList();
            }

            var ratings = await _dbContext.Ratings
                .Include(r => r.OrderItem)
                .Where(r => orderItemIds.Contains(r.OrderItem.Id))
                .ToListAsync();

            foreach(var order in orders)
            {
                foreach(var item in order.Items)
                {
                    var prodRatings = ratings.Where(r => r.OrderItem.Id == item.Id);

                    var productCopy = JsonSerializer.Serialize(item.Product);
                    item.Product = JsonSerializer.Deserialize<Product>(productCopy);

                    item.Product.AvgRating = prodRatings.Any() ?
                        prodRatings.Sum(r => r.Rate) / prodRatings.Count()
                        : 0;
                }
            }

            return orders;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order> AddOrder(Order order)
        {
            var createdOrderItems = new List<OrderItem>();

            foreach(var item in order.Items)
            {
                var productEntity = await _dbContext.Products.FindAsync(item.Product.Id);
                item.Product = productEntity;
                var itemEntity = await _dbContext.OrderItems.AddAsync(item);
                createdOrderItems.Add(itemEntity.Entity);
            }

            order.Items = createdOrderItems;
            order.CreatedDate = DateTime.Now;

            order.Customer = await _dbContext.Users.FindAsync(order.Customer.Id);
            var result = await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteOrder(int orderId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateOrder(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
