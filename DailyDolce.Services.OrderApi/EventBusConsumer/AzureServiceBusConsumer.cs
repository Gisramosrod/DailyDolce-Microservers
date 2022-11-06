using Azure.Messaging.ServiceBus;
using DailyDolce.MessageBus;
using DailyDolce.Services.OrderApi.Dtos;
using DailyDolce.Services.OrderApi.Services;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace DailyDolce.Services.OrderApi.EventBusConsumer {
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer {
        private readonly OrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly IMessageBus _messageBus;
        private readonly string _serviceBusConnectionString;
        private readonly string _checkoutMessageTopic;
        private readonly string _orderSubscription;
        private readonly string _paymentProcessTopic;
        private readonly string _updatePaymentStatusTopic;        
        private readonly ServiceBusProcessor _checkoutProcessor;
        private readonly ServiceBusProcessor _updatePaymentStatusProcessor;

        public AzureServiceBusConsumer(
            OrderService orderService,
            IConfiguration configuration,
            IMessageBus messageBus
            ) {

            _orderService = orderService;
            _configuration = configuration;
            _messageBus = messageBus;

            _serviceBusConnectionString = _configuration.GetSection("AzureServiceBus").GetSection("ConnectionString").Value;
            _checkoutMessageTopic = _configuration.GetSection("AzureServiceBus").GetSection("CheckoutMessageTopic").Value;
            _orderSubscription = _configuration.GetSection("AzureServiceBus").GetSection("OrderSubscription").Value;
            _paymentProcessTopic = _configuration.GetSection("AzureServiceBus").GetSection("PaymentProcessTopic").Value;
            _updatePaymentStatusTopic = _configuration.GetSection("AzureServiceBus").GetSection("UpdatePaymentStatusTopic").Value;

            var client = new ServiceBusClient(_serviceBusConnectionString);
            _checkoutProcessor = client.CreateProcessor(_checkoutMessageTopic, _orderSubscription);
            _updatePaymentStatusProcessor = client.CreateProcessor(_updatePaymentStatusTopic, _orderSubscription);
        }

        public async Task Start() {
            _checkoutProcessor.ProcessMessageAsync += OnCheckoutMessageReceived;
            _checkoutProcessor.ProcessErrorAsync += ErrorHandler;
            await _checkoutProcessor.StartProcessingAsync();

            _updatePaymentStatusProcessor.ProcessMessageAsync += OnOrderPaymentUpdateReceived;
            _updatePaymentStatusProcessor.ProcessErrorAsync += ErrorHandler;
            await _updatePaymentStatusProcessor.StartProcessingAsync();
        }

        public async Task Stop() {
            await _checkoutProcessor.StopProcessingAsync();
            await _checkoutProcessor.DisposeAsync();

            await _updatePaymentStatusProcessor.StopProcessingAsync();
            await _updatePaymentStatusProcessor.DisposeAsync();
        }

        private async Task OnCheckoutMessageReceived(ProcessMessageEventArgs args) {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            var orderDto = JsonConvert.DeserializeObject<OrderDto>(body);
            var orderId = await _orderService.AddOrder(orderDto);

            PaymentRequestDto paymentRequestDto = new() {
                OrderId = orderId,
                FirstName = orderDto.FirstName,
                LastName = orderDto.LastName,
                CardNumber = orderDto.CardNumber,
                CVC = orderDto.CVC,
                ExpirationDate = orderDto.ExpirationDate
            };

            try {
                await _messageBus.PublishMessage(paymentRequestDto, _paymentProcessTopic);
                await args.CompleteMessageAsync(args.Message);

            } catch (Exception ex) { throw; }
        }

        private async Task OnOrderPaymentUpdateReceived (ProcessMessageEventArgs args) {

            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            PaymentResultDto paymentResult = JsonConvert.DeserializeObject<PaymentResultDto>(body);

            await _orderService.UpdateOrderPaymentStatus(paymentResult.OrderId, paymentResult.Status);
            await args.CompleteMessageAsync(args.Message);
        }

        private Task ErrorHandler(ProcessErrorEventArgs args) {
            Console.WriteLine(args.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
