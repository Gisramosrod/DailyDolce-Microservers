using DailyDolce.Service.OrderApi.Dtos;
using DailyDolce.Service.OrderApi.Models;

namespace DailyDolce.Service.OrderApi.Services {
    public interface IOrderService {
        Task<bool> AddOrder(OrderDto orderDto);
        Task<bool> UpdateOrderPaymentStatus(int orderId, bool isPaid);
    }
}
