using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopApi.Models;
using WebShopApi.Services;
using WebShopApi.Services.Implementation;

namespace WebShopApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _ordersService;
        private readonly IUsersService _usersService;

        public OrdersController(IOrderService ordersService, IUsersService usersService)
        {
            _ordersService = ordersService;
            _usersService = usersService;
        }


        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] Order order)
        {
            try
            {
                var createdOrder = await _ordersService.AddOrder(order);

                return Ok(createdOrder);
            }
            catch (Exception ex)
            {
                return Problem("Internal server error!");
            }
        }

        [HttpGet("my")]
        public async Task<ActionResult<IEnumerable<Order>>> GetMyOrders()
        {
            try
            {
                var loggedUser = await _usersService.GetLoggedUserByEmail(HttpContext.User.Identity.Name);
                var orders = await _ordersService.GetOrdersByUser(loggedUser.Id);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem("Internal server error!");
            }
        }

        [HttpPost("orderitem/{orderItemId}/rate")]
        public async Task<ActionResult<IEnumerable<ProductFilterOptions>>> Rate([FromRoute] int orderItemId, [FromBody] int rate)
        {
            try
            {
                var loggedUser = await _usersService.GetLoggedUserByEmail(HttpContext.User.Identity.Name);
                await _ordersService.RateOrder(orderItemId, loggedUser.Id, rate);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem("Internal server error!");
            }
        }
    }
}
