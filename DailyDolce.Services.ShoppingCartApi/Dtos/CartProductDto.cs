namespace DailyDolce.Services.ShoppingCartApi.Dtos {
    public class CartProductDto {
        public int Id { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
