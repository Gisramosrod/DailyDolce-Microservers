using Azure.Messaging.ServiceBus;
using DailyDolce.MessageBus;
using DailyDolce.Services.Payment.Messages;
using Newtonsoft.Json;
using PaymentProcessorSolution;
using System.Text;

namespace DailyDolce.Services.Payment.EventBusConsumer {
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer{
        private readonly string _serviceBusConnectionString;
        private readonly string _orderSubscription;
        private readonly string _paymentProcessTopic;
        private readonly string _updatePaymentStatusTopic;

        private readonly ServiceBusProcessor _paymentServiceBusProcessor;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IConfiguration _configuration;
        private readonly IMessageBus _messageBus;

        public AzureServiceBusConsumer(
            IPaymentProcessor paymentProcessor, 
            IConfiguration configuration, 
            IMessageBus messageBus
            ) {

            _paymentProcessor = paymentProcessor;
            _configuration = configuration;
            _messageBus = messageBus;

            _serviceBusConnectionString = _configuration.GetSection("AzureServiceBus").GetSection("ConnectionString").Value;
            _orderSubscription = _configuration.GetSection("AzureServiceBus").GetSection("OrderSubscription").Value;
            _paymentProcessTopic = _configuration.GetSection("AzureServiceBus").GetSection("PaymentProcessTopic").Value;
            _updatePaymentStatusTopic = _configuration.GetSection("AzureServiceBus").GetSection("UpdatePaymentStatusTopic").Value;

            var client = new ServiceBusClient(_serviceBusConnectionString);
            _paymentServiceBusProcessor = client.CreateProcessor(_paymentProcessTopic, _orderSubscription);
        }

        public async Task Start() {
            _paymentServiceBusProcessor.ProcessMessageAsync += ProcessPayment;
            _paymentServiceBusProcessor.ProcessErrorAsync += ErrorHandler;
            await _paymentServiceBusProcessor.StartProcessingAsync();
        }

        public async Task Stop() {
            await _paymentServiceBusProcessor.StopProcessingAsync();
            await _paymentServiceBusProcessor.DisposeAsync();
        }

        Task ErrorHandler(ProcessErrorEventArgs args) {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task ProcessPayment(ProcessMessageEventArgs args) {

            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            PaymentRequestMessage paymentRequestMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(body);
            var result = _paymentProcessor.ProcessPayment();

            PaymentResponseMessage paymentResponseMessage = new() {
                Status = result,
                OrderId = paymentRequestMessage.OrderId,
            };

            try {
                await _messageBus.PublishMessage(paymentResponseMessage, _updatePaymentStatusTopic);
                await args.CompleteMessageAsync(args.Message);

            } catch (Exception ex) { throw; }
        }
    }

}
