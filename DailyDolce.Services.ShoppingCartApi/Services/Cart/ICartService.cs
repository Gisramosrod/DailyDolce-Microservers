using DailyDolce.Services.ShoppingCartApi.Dtos;

namespace DailyDolce.Services.ShoppingCartApi.Services.Cart {
    public interface ICartService {
        Task<GetCartDto> GetCartByUserId(string userId);
        Task<bool> AddProductToCart(AddToCartDto addProductToCartDto);
        Task<bool> RemoveFromCart(int cartProductId);
        //Task<bool> ClearCart(string userId);
    }
}
