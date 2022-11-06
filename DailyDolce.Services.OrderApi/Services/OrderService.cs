using AutoMapper;
using DailyDolce.Services.OrderApi.Data;
using DailyDolce.Services.OrderApi.Dtos;
using DailyDolce.Services.OrderApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyDolce.Services.OrderApi.Services {
    public class OrderService : IOrderService {
        private readonly DbContextOptions<DataContext> _contextOptions;
        private readonly IMapper _mapper;

        public OrderService(DbContextOptions<DataContext> contextOptions, IMapper mapper) {
            _contextOptions = contextOptions;
            _mapper = mapper;
        }

        public async Task<int> AddOrder(OrderDto orderDto) {

            await using var _context = new DataContext(_contextOptions);

            var orderUser = _mapper.Map<OrderUserModel>(orderDto);
            var orderProducts = new List<OrderProductModel>();
            foreach(var orderProductDto in orderDto.CartProductsDto) {
                orderProducts.Add(new OrderProductModel() {
                    ProductId = orderProductDto.Product.Id,
                    Quantity = orderProductDto.Quantity
                });
            }
            var order = _mapper.Map<OrderModel>(orderDto);
            order.OrderUser = orderUser;
            order.OrderProducts = orderProducts;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order.Id;
        }

        public async Task<bool> UpdateOrderPaymentStatus(int orderId, bool isPaid) {

            await using var _context = new DataContext(_contextOptions);
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            if(order == null) return false;

            order.PaymentStatus = isPaid;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
