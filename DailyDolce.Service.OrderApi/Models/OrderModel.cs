namespace DailyDolce.Service.OrderApi.Models {
    public class OrderModel {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
        public double TotalOrder { get; set; }
        public OrderUserModel OrderUser { get; set; }
        public List<OrderProductModel> OrderProducts { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
