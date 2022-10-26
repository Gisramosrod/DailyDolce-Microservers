namespace DailyDolce.Services.ShoppingCartApi.Models {
    public class CartModel {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public List<CartProductModel> CartProducts { get; set; }
    }
}
