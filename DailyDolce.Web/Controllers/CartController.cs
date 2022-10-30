using DailyDolce.Web.Dtos;
using DailyDolce.Web.Dtos.Cart;
using DailyDolce.Web.Dtos.Coupon;
using DailyDolce.Web.Services.Cart;
using DailyDolce.Web.Services.Coupon;
using DailyDolce.Web.Services.Product;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DailyDolce.Web.Controllers {

    [Authorize]
    public class CartController : Controller {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;

        public CartController(
            IProductService productService, 
            ICartService cartService,
            ICouponService couponService) {

            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }

        public async Task<IActionResult> CartIndex() {
            return View(await GetCart());
        }

        private async Task<CartDto> GetCart() {

            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var cartResponse = await _cartService.GetCartByUserId<ResponseDto>(userId, accessToken);

            CartDto cartDto = new();
            if (cartResponse.Data != null && cartResponse.Success) {
                cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(cartResponse.Data));

                foreach (var cartProductDto in cartDto.CartProductsDto) {
                    cartDto.TotalOrder += (cartProductDto.Product.Price * cartProductDto.Quantity);
                };

                if (!string.IsNullOrEmpty(cartDto.CouponCode)) {
                    var couponReponse = await _couponService.GetCouponByCode<ResponseDto>(cartDto.CouponCode, accessToken);

                    if (couponReponse.Data != null && couponReponse.Success) {
                        CouponDto couponDto = JsonConvert.
                            DeserializeObject<CouponDto>(Convert.ToString(couponReponse.Data));
                        cartDto.Discount = couponDto.Discount;
                        cartDto.TotalOrder -= couponDto.Discount;
                    }
                }
            }
            return cartDto;
        }

        public async Task<IActionResult> RemoveFromCart(int cartProductId) {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveFromCart<ResponseDto>(cartProductId, accessToken);

            if (response.Data != null && response.Success)
                return RedirectToAction(nameof(CartIndex));

            return View();
        }       

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto) {

            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.ApplyCoupon<ResponseDto>(cartDto, accessToken);

            if (response.Data != null && response.Success)
                return RedirectToAction(nameof(CartIndex));

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto) {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveCoupon<ResponseDto>(cartDto, accessToken);

            if (response.Data != null && response.Success)
                return RedirectToAction(nameof(CartIndex));

            return View();
        }

        public async Task<IActionResult> Checkout() {

            var cartDto = await GetCart();

            OrderDto orderDto = new() {
                UserId = cartDto.UserId,
                CouponCode = cartDto.CouponCode,
                Discount = cartDto.Discount,
                TotalOrder = cartDto.TotalOrder,
                CartProductsDto = cartDto.CartProductsDto
            };

            return View(orderDto);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderDto orderDto) {

            var cartDto = await GetCart();

            orderDto.UserId = cartDto.UserId;
            orderDto.CouponCode = cartDto.CouponCode;
            orderDto.Discount = cartDto.Discount;
            orderDto.TotalOrder = cartDto.TotalOrder;
            orderDto.CartProductsDto = cartDto.CartProductsDto;           

            try {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response =  await _cartService.Checkout<ResponseDto>(orderDto);
                return RedirectToAction(nameof(Confirmation));

            } catch { return View(orderDto); }
        }

        public IActionResult Confirmation() {
            return View();
        }
        
    }
}
