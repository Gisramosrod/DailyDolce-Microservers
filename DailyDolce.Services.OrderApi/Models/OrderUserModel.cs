namespace DailyDolce.Services.OrderApi.Models {
    public class OrderUserModel {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVC { get; set; }
        public string ExpirationDate { get; set; }
        public List<OrderModel> Orders { get; set; }
    }
}
