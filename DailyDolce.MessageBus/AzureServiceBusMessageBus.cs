using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDolce.MessageBus {
    public class AzureServiceBusMessageBus : IMessageBus {

        private readonly string _connectionString = "Endpoint=sb://dailydolce.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=XbPFaxQEG3JAQRq/MwPTHEiBTAa4jH7yRrW29dFi+EU=";
        public async Task PublishMessage(BaseMessage message, string topicName) {

            await using var client = new ServiceBusClient(_connectionString);
            ServiceBusSender sender = client.CreateSender(topicName);

            var finalMessage = new ServiceBusMessage(
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))) {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();
        }
    }
}
