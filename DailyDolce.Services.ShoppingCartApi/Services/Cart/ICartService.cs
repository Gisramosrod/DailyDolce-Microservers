using DailyDolce.Services.ShoppingCartApi.Dtos;

namespace DailyDolce.Services.ShoppingCartApi.Services.Cart {
    public interface ICartService {
        Task<CartDto> GetCartByUserId(string userId);
        Task<bool> AddToCart(AddToCartDto addProductToCartDto);
        Task<bool> RemoveFromCart(int cartProductId);
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);

        //Task<bool> ClearCart(string userId);
    }
}
