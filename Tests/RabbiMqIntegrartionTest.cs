using ApiSample.Domain;
using RabbitMQManager;
using System.Text.Json;

namespace Tests
{
    public class RabbiMqIntegrartionTest
    {
        Manager? _queueManager;
        string fanoutName = "fanoutTest";
        string queueName = "clientQueueTest";

        public RabbiMqIntegrartionTest()
        {
            _queueManager = new Manager("guest", "guest");

            _queueManager.CreateExchangeFanout(fanoutName, true, _queueManager.Connection);
            _queueManager.CreateQueue(queueName, _queueManager.Connection);
            _queueManager.BindingQueue(queueName, fanoutName, _queueManager.Connection);
        }

        [Fact]
        public void MustEnqueueItem() 
        {
            string itemJson = JsonSerializer.Serialize(new Feed { Id = "123456", Title = "Teste" });
            _queueManager?.Enqueue(itemJson, _queueManager.Connection, fanoutName);
            int? itemsCount = _queueManager?.GetQueueMessageCount(queueName);
            _queueManager?.PurgeQueue(queueName);

            Assert.True(itemsCount > 0);
        }
    }
}
