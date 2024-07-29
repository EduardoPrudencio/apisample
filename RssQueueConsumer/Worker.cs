using MongoDB.Driver;
using RabbitMQManager;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using ApiSample.Domain;
using ApiSample.Infraestrutura;

namespace RssQueueConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConnection? _connection;
        private IModel? _channel;
        private Manager _queueManager;
        private EventingBasicConsumer? _consumer;
        private ImageDownloader _imageDownloader;

        IMongoDatabase? database;

        private const string QueueName = "omnycontent";
        private readonly MongoDBIntegrate _mongoDBIntegrate;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _queueManager = new Manager("guest", "guest", host: "localhost");
            _imageDownloader = new ImageDownloader();
            _mongoDBIntegrate = new MongoDBIntegrate("mongodb://root:123456@localhost:27017", "mudb");
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            database = _mongoDBIntegrate.GetDatabaseConnection();

            _connection = _queueManager.Connection;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            _consumer = _queueManager.CreateConsumer(_channel);
            _consumer.Received += Consumer_Received;
            _channel.BasicQos(0, 1, false);
            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: _consumer);

            return base.StartAsync(cancellationToken);
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Feed _feed = JsonSerializer.Deserialize<Feed>(message);

            if (_feed.Id != null)
            {
                if (_feed.Id.Contains("http"))
                {
                    string[] parts = _feed.Id.Split("/");
                    _feed.Id = string.IsNullOrEmpty(parts.Last()) ? parts[parts.Count() - 2] : parts.Last();
                }

                Task downloadImage = Task.Run(() => _imageDownloader.SaveImageAsync(_feed.Image, _feed.Id));
                downloadImage.Wait();
            }

            _logger.LogInformation(" [x] Received {0}", message);

            try
            {
                var collection = database?.GetCollection<Feed>("feeds");
                collection?.InsertOne(_feed);

                _channel?.BasicAck(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
                _channel?.BasicNack(e.DeliveryTag, false, false);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _channel?.Close();
            _connection?.Close();
            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
