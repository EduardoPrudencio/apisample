using RssReader;
using RabbitMQManager;
using System.Text.Json;

namespace RssReaderContainer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private Reader _reader;
        IEnumerable<Feed> _list;

        static Manager? _queueManager;
        string fanoutName = "fanoutFeed";
        string queueName = "omnycontent";
        string[] feedIds;

        public Worker(ILogger<Worker> logger)
        {
            string _rabbitConnectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTIONSTRING") ?? "localhost";

            _logger = logger;
            _queueManager = new Manager("guest", "guest", host: _rabbitConnectionString);
            _reader = new Reader();

            _queueManager.CreateExchangeFanout(fanoutName, true, _queueManager.Connection);
            _queueManager.CreateQueue(queueName, _queueManager.Connection);
            _queueManager.BindingQueue(queueName, fanoutName, _queueManager.Connection);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _list = _reader.Parse("https://www.omnycontent.com/d/playlist/42233656-1562-49af-98d5-acd100df7932/a3504d75-e95a-41c5-8dda-aced013a0cb9/343561ea-888c-4641-bd55-aced013a0cd5/podcast.rss");

                if (feedIds == null) feedIds = new string[_list.Count()];

                _list = _list.Where(f => !feedIds.Contains(f.Id));

                try
                {
                    JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };

                    int count = 0;

                    foreach (var feed in _list)
                    {
                        string feedJson = JsonSerializer.Serialize(feed, jsonOptions);
                        _logger.LogInformation($"Feed: {feedJson}");

                        if (feed.Id != null && (Array.IndexOf(feedIds, feed.Id) < 0))
                        {
                            _queueManager?.Enqueue(feedJson, _queueManager.Connection, fanoutName);
                            feedIds[count] = feed.Id;
                            count++;
                        }
                    }

                    _logger.LogInformation("A ista possui: {time} itens.", _list.Count());
                    await Task.Delay(43200, stoppingToken);
                }
                catch (Exception exp)
                {
                    _logger.LogError($"Erro: {exp.Message} - {exp.StackTrace}");
                }
            }
        }
    }
}