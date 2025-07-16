using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopApi.Models;
using WebShopApi.Services;
using WebShopApi.Services.Implementation;

namespace WebShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productsService;
        private readonly IUsersService _usersService;
        public ProductsController(IProductService productsService, IUsersService usersService)
        {
            this._productsService = productsService;
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string? search, [FromQuery] int[]? brandIds, [FromQuery] int[]? categoryIds, [FromQuery] string[]? sizes, [FromQuery] string[]? categoryNames)
        {
            try
            {
                var products = await _productsService.GetProductsByFilters(search, brandIds, categoryIds, sizes, categoryNames);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return Problem("Internal server error!");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductById(int id)
        {
            try
            {
                var product = await _productsService.GetProductById(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return Problem("Internal server error!");
            }
        }

        [HttpGet("filters")]
        public async Task<ActionResult<IEnumerable<ProductFilterOptions>>> GetProductFilterOptions()
        {
            try
            {
                var productFilterOptions = await _productsService.GetProductFilterOptions();

                return Ok(productFilterOptions);
            }
            catch (Exception ex)
            {
                return Problem("Internal server error!");
            }
        }

        [HttpGet("{productId}/ratings")]
        public async Task<ActionResult<IEnumerable<ProductRating>>> GetProductRatings([FromRoute] int productId)
        {
            try
            {
                var productRatings = await _productsService.GetProductRatings(productId);

                return Ok(productRatings);
            }
            catch (Exception ex)
            {
                return Problem("Internal server error!");
            }
        }
    }
}
