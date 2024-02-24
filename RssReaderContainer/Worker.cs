using RssReader;
using System.Formats.Asn1;
using System;

namespace RssReaderContainer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private Reader _reader;

        IList<Feed> _list;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            _reader = new Reader();

            _list = _reader.Parse("https://www.omnycontent.com/d/playlist/42233656-1562-49af-98d5-acd100df7932/a3504d75-e95a-41c5-8dda-aced013a0cb9/343561ea-888c-4641-bd55-aced013a0cd5/podcast.rss");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // ENFILEIRAR OBJETOS NO RABBITMQ        
                foreach (var feed in _list)
                {
                    _logger.LogInformation($"Titulo {feed.Title}", feed.Title);
                    _logger.LogInformation($"Conteudo {feed.Content}", feed.Content);
                    _logger.LogInformation($"Data de publicao {feed.PublishDate}", feed.PublishDate);
                }

                _logger.LogInformation("A ista possui: {time} itens.", _list.Count());
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}