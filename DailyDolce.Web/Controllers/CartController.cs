using DailyDolce.Web.Dtos;
using DailyDolce.Web.Dtos.Cart;
using DailyDolce.Web.Services.Cart;
using DailyDolce.Web.Services.Product;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DailyDolce.Web.Controllers {
    public class CartController : Controller {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CartController(IProductService productService, ICartService cartService) {
            _productService = productService;
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex() {
            return View(await GetCart());
        }

        [Authorize]
        public async Task<IActionResult> Remove(int cartProductId) {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveFromCart<ResponseDto>(cartProductId, accessToken);

            if (response.Data != null && response.Success)
                return RedirectToAction(nameof(CartIndex));

            return View();
        }

        private async Task<GetCartDto> GetCart() {

            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.GetCartByUserId<ResponseDto>(userId, accessToken);

            GetCartDto getCartDto = new();
            if (response.Data != null && response.Success) {
                getCartDto = JsonConvert.DeserializeObject<GetCartDto>(Convert.ToString(response.Data));

                foreach (var cartProductDto in getCartDto.CartProductsDto) {
                    getCartDto.TotalOrder += (cartProductDto.Product.Price * cartProductDto.Quantity);
                };
            }
            return getCartDto;
        }
    }
}
