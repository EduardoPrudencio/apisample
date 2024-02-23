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

            _list = _reader.Parse("https://www.tecmundo.com.br/rss");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                foreach (var feed in _list)
                {
                    _logger.LogInformation("T�tulo", feed.Title);
                    _logger.LogInformation("Conte�do", feed.Content);
                    _logger.LogInformation("Data de publica��o", feed.PublishDate);
                }


                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}