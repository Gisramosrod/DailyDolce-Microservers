using DailyDolce.Web.Dtos.Cart;
using DailyDolce.Web.Dtos.Product;

namespace DailyDolce.Web.Services.Cart {
    public interface ICartService {
        Task<T> GetCartByUserId<T>(string userId, string token = null);
        Task<T> AddToCart<T>(AddToCartDto addToCartDto, string token = null);
        Task<T> RemoveFromCart<T>(int cartProductId, string token = null);
        Task<T> ApplyCoupon<T>(CartDto cartDto, string token = null);
        Task<T> RemoveCoupon<T>(CartDto cartDto, string token = null);

    }
}
