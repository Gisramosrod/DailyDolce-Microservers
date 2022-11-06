namespace DailyDolce.Services.OrderApi.Models {
    public class OrderProductModel {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public OrderModel Order { get; set; }
    }
}
