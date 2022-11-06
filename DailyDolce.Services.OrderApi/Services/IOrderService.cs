using DailyDolce.Services.OrderApi.Dtos;
using DailyDolce.Services.OrderApi.Models;

namespace DailyDolce.Services.OrderApi.Services {
    public interface IOrderService {
        Task<int> AddOrder(OrderDto orderDto);
        Task<bool> UpdateOrderPaymentStatus(int orderId, bool isPaid);
    }
}
