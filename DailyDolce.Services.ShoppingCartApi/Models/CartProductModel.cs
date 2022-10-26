using DailyDolce.Services.ShoppingCartApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyDolce.Services.ShoppingCartApi.Models {
    public class CartProductModel {
        public int Id { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
        public CartModel Cart { get; set; }
    }
}
