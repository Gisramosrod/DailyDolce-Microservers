using DailyDolce.Web.Dtos.Cart;
using DailyDolce.Web.Models;
using DailyDolce.Web.Services.Base;

namespace DailyDolce.Web.Services.Cart {
    public class CartService : BaseService, ICartService {

        public CartService(IHttpClientFactory clientFactory) : base(clientFactory) {
        }

        public async Task<T> GetCartByUserId<T>(string userId, string token = null) {
            return await SendRequest<T>(new ApiRequest() {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/cart/{userId}",
                AccessToken = token
            });
        }

        public async Task<T> AddToCart<T>(AddToCartDto addToCartDto, string token = null) {
            return await SendRequest<T>(new ApiRequest() {
                ApiType = SD.ApiType.POST,
                Data = addToCartDto,
                Url = $"{SD.GatewayBaseUrl}/api/cart",
                AccessToken = token
            });
        }

        public async Task<T> RemoveFromCart<T>(int cartProductId, string token = null) {
            return await SendRequest<T>(new ApiRequest() {
                ApiType = SD.ApiType.DELETE,
                Data = cartProductId,
                Url = $"{SD.GatewayBaseUrl}/api/cart/{cartProductId}",
                AccessToken = token
            });
        }

        public async Task<T> ApplyCoupon<T>(CartDto cartDto, string token = null) {
            return await SendRequest<T>(new ApiRequest() {
                ApiType = SD.ApiType.PUT,
                Data = cartDto,
                Url = $"{SD.GatewayBaseUrl}/api/cart/applyCoupon",
                AccessToken = token
            });
        }

        public async Task<T> RemoveCoupon<T>(CartDto cartDto, string token = null) {
            return await SendRequest<T>(new ApiRequest() {
                ApiType = SD.ApiType.PUT,
                Data = cartDto,
                Url = $"{SD.GatewayBaseUrl}/api/cart/removeCoupon",
                AccessToken = token
            });
        }

        public async Task<T> Checkout<T>(OrderDto orderDto, string token = null) {
            return await SendRequest<T>(new ApiRequest() {
                ApiType = SD.ApiType.POST,
                Data = orderDto,
                Url = $"{SD.GatewayBaseUrl}/api/cart/checkout",
                AccessToken = token
            });
        }
    }
}
