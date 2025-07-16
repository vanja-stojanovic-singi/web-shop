using WebShopApi.Models;
using WebShopApi.Repositories;
using WebShopApi.Repositories.Implementation;

namespace WebShopApi.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IProductItemRepository _productItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderService(IOrderRepository repository, IProductItemRepository productItemRepository, IOrderItemRepository orderItemRepository, IUserRepository userRepository, IRatingRepository rateRepository) 
        {
            _repository = repository;
            _productItemRepository = productItemRepository;
            _orderItemRepository = orderItemRepository;
            _userRepository = userRepository;
            _ratingRepository = rateRepository;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _repository.GetOrders();
        }

        public Task<IEnumerable<Order>> GetOrdersByUser(Guid userId)
        {
            return _repository.GetOrdersForUser(userId);
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _repository.GetOrderById(orderId);
        }

        public async Task<Order> AddOrder(Order order)
        {
            try
            {
                var result = await _repository.AddOrder(order);

                foreach (var item in order.Items)
                {
                    await _productItemRepository.DeleteProductItem(item.Product.Id, item.Size);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteOrder(int orderId)
        {
            await _repository.DeleteOrder(orderId);
        }

        public async Task UpdateOrder(Order order)
        {
            await _repository.UpdateOrder(order);
        }
        public async Task RateOrder(int orderItemId, Guid userId, int rate)
        {
            var user = await _userRepository.GetUserById(userId);
            var orderItem = await _orderItemRepository.GetOrderItemById(orderItemId);

            if (user == null || orderItem == null)
            {
                return;
            }

            await _ratingRepository.AddRating(new Rating
            {
                Customer = user,
                Rate = rate,
                OrderItem = orderItem
            });
        }
    }
}
