using DailyDolce.Services.ShoppingCartApi.Dtos;
using DailyDolce.Services.ShoppingCartApi.Services.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyDolce.Services.ShoppingCartApi.Controllers {
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase {
        private readonly ICartService _cartService;
        protected ResponseDto _response;

        public CartController(ICartService cartService) {
            _response = new ResponseDto();
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseDto>> GetCart(string userId) {

            try {
                _response.Data = await _cartService.GetCartByUserId(userId);

            } catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> AddToCart(AddToCartDto addToCartDto) {

            try {
                _response.Data = await _cartService.AddProductToCart(addToCartDto);
            } catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpDelete("{cartProductId}")]
        public async Task<ActionResult<ResponseDto>> RemoveFromCart(int cartProductId) {

            try {
                _response.Data = await _cartService.RemoveFromCart(cartProductId);
            } catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }
            return _response;
        }
        /*
        [HttpPut]
        public async Task<ActionResult<ResponseDto>> UpdateCard(CartDto updatedCart) {

            try {
                _response.Data = await _cartRepository.CreateUpdateCart(updatedCart);
            } catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpDelete("{cardId}")]
        public async Task<ActionResult<ResponseDto>> DeleteCart(int id) {

            try {
                _response.Data = await _cartRepository.RemoveFromCart(id);
            } catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }
            return _response;
        }*/
    }
}
