using DailyDolce.MessageBus;
using DailyDolce.Services.ShoppingCartApi.Dtos;
using DailyDolce.Services.ShoppingCartApi.Services.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyDolce.Services.ShoppingCartApi.Controllers {
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase {
        private readonly ICartService _cartService;
        private readonly IMessageBus _messageBus;
        protected ResponseDto _response;

        public CartController(ICartService cartService, IMessageBus messageBus) {
            _response = new ResponseDto();
            _cartService = cartService;
            _messageBus = messageBus;
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
                _response.Data = await _cartService.AddToCart(addToCartDto);
            } catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }
            return _response;
        }

        //este es un update tambien
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

        [HttpPut("applyCoupon")]
        public async Task<ActionResult<ResponseDto>> ApplyCoupon(CartDto cartDto) {

            try {
                _response.Data = await _cartService.ApplyCoupon(cartDto.UserId, cartDto.CouponCode);
            } catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpPut("removeCoupon")]
        public async Task<ActionResult<ResponseDto>> RemoveCoupon(CartDto cardDto) {

            try {
                _response.Data = await _cartService.RemoveCoupon(cardDto.UserId);
            } catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<ResponseDto>> Checkout (OrderDto orderDto) {
            try {
                if (orderDto.CartDto == null) {
                    _response.Success = false;
                    return BadRequest(_response);
                }

                await _messageBus.PublishMessage(orderDto, "checkoutmessagetopic");

            }catch(Exception ex) {

            }
            return _response;
        }

    }
}
