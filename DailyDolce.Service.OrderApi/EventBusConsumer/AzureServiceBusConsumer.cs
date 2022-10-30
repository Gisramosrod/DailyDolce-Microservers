using Azure.Messaging.ServiceBus;
using DailyDolce.Service.OrderApi.Dtos;
using DailyDolce.Service.OrderApi.Services;
using Newtonsoft.Json;
using System.Text;

namespace DailyDolce.Service.OrderApi.EventBusConsumer {
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer {
        private readonly OrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly string serviceBusConnectionString;
        private readonly string checkoutMessageTopic;
        private readonly string orderSubscription;
        private ServiceBusProcessor checkoutProcessor;

        public AzureServiceBusConsumer(OrderService orderService, IConfiguration configuration) {
            _orderService = orderService;
            _configuration = configuration;

            serviceBusConnectionString = _configuration.GetSection("AzureServiceBus").GetSection("ConnectionString").Value;
            checkoutMessageTopic = _configuration.GetSection("AzureServiceBus").GetSection("CheckoutMessageTopic").Value;
            orderSubscription = _configuration.GetSection("AzureServiceBus").GetSection("OrderSubscription").Value;

            var client = new ServiceBusClient(serviceBusConnectionString);
            checkoutProcessor = client.CreateProcessor(checkoutMessageTopic, orderSubscription);
        }

        public async Task Start() {
            checkoutProcessor.ProcessMessageAsync += OnCheckoutMessageReceived;
            checkoutProcessor.ProcessErrorAsync += ErrorHandler;
            await checkoutProcessor.StartProcessingAsync();
        }

        public async Task Stop() {
            await checkoutProcessor.StopProcessingAsync();
            await checkoutProcessor.DisposeAsync();
        }

        private async Task OnCheckoutMessageReceived(ProcessMessageEventArgs args) {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            var orderDto = JsonConvert.DeserializeObject<OrderDto>(body);
            await _orderService.AddOrder(orderDto);
        }

        private Task ErrorHandler(ProcessErrorEventArgs args) {
            Console.WriteLine(args.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
