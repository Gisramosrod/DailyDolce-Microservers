using AutoMapper;
using DailyDolce.Services.ShoppingCartApi.Models;
using DailyDolce.Services.ShoppingCartApi.Data;
using DailyDolce.Services.ShoppingCartApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DailyDolce.Services.ShoppingCartApi.Services.Cart {

    public class CartService : ICartService {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CartService(DataContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddProductToCart(AddToCartDto addToCartDto) {

            var productToAddDto = addToCartDto.ProductDto;
            var dbProductToAdd = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productToAddDto.Id);

            if (dbProductToAdd == null) {
                dbProductToAdd = _mapper.Map<ProductModel>(productToAddDto);
                _context.Products.Add(dbProductToAdd);
                await _context.SaveChangesAsync();

            }//else verificar q no haya cambiado, si no modificarlo

            var cartUserId = addToCartDto.UserId;
            var dbCartOfUser = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == cartUserId);

            if (dbCartOfUser == null) {
                dbCartOfUser = new CartModel() {
                    UserId = cartUserId,
                };
                _context.Carts.Add(dbCartOfUser);
                await _context.SaveChangesAsync();

                var cartProduct = new CartProductModel {
                    Product = dbProductToAdd,
                    Quantity = addToCartDto.Quantity,
                    Cart = dbCartOfUser
                };
                _context.CartProducts.Add(cartProduct);
                await _context.SaveChangesAsync();

            } else {
                var dbCartProduct = await _context.CartProducts
                    .FirstOrDefaultAsync(cp => cp.Cart.Id == dbCartOfUser.Id &&
                    cp.Product.Id == dbProductToAdd.Id);

                if (dbCartProduct == null) {
                    var cartProduct = new CartProductModel {
                        Product = dbProductToAdd,
                        Quantity = addToCartDto.Quantity,
                        Cart = dbCartOfUser
                    };
                    _context.CartProducts.Add(cartProduct);
                    await _context.SaveChangesAsync();

                } else {
                    dbCartProduct.Quantity += addToCartDto.Quantity;
                    _context.CartProducts.Update(dbCartProduct);
                    await _context.SaveChangesAsync();
                }
            }

            return true;
        }

        public async Task<GetCartDto> GetCartByUserId(string userId) {

            var cart = await _context.Carts
                .Include(c => c.CartProducts).ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return null;

            var cartProductsDto = _mapper.Map<List<CartProductDto>>(cart.CartProducts);
            var cartDto = _mapper.Map<GetCartDto>(cart);
            cartDto.CartProductsDto = cartProductsDto;

            return cartDto;
        }

        public async Task<bool> RemoveFromCart(int cartProductId) {            

            var cartProduct = await _context.CartProducts
                .FirstOrDefaultAsync(cp => cp.Id == cartProductId);

            if (cartProduct == null) return false;

            _context.CartProducts.Remove(cartProduct);
            await _context.SaveChangesAsync();
            return true;
        }

        /*
          public async Task<GetCartDto> GetCartByUserId(string userId) {

            var cart = await _context.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return null;

            List<ProductDto> productsDto = new ();
            foreach (var cartProduct in cart.CartProducts) {

                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == cartProduct.Id);

                if (product != null) {
                    var productDto = _mapper.Map<ProductDto>(product);
                    productDto.Quantity = cartProduct.Quantity;
                    productsDto.Add(productDto);                   
                }            
            }

            var cartDto = _mapper.Map<GetCartDto>(cart);
            cartDto.ProductsDto = productsDto;

            return cartDto;
        }
         */

        /*


 public async Task<bool> ClearCart(string userId) {

     var dbCartUser = await _context.CartUser
         .FirstOrDefaultAsync(ch => ch.UserId == userId);

     if (dbCartUser == null) return false;

     _context.CartProducts
         .RemoveRange(_context.CartProducts
         .Where(cd => cd.CartUserId == dbCartUser.Id));

     _context.CartUser.Remove(dbCartUser);
     await _context.SaveChangesAsync();

     return true;
 }*/

    }
}



