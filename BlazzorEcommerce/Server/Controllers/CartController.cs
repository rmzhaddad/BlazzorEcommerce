using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazzorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("Products")]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> GetCartProducts(List<CartItem> cartItems)
        {
            var result = await _cartService.GetCartProducts(cartItems);
            return Ok(result);
        }
        [HttpPost()]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> StoreCartItems(List<CartItem> cartItems)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _cartService.StoreCartItems(cartItems);
            return Ok(result);
        }
        [HttpGet("count")]
        public async Task<ActionResult<ServiceResponse<int>>> GetCartItemsCount()
        {
            return  await _cartService.GetCartItemsCount();
        }
    }
}
