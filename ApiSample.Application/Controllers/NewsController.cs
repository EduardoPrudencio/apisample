using ApiSample.Application.Model;
using ApiSample.Domain;
using ApiSample.Infraestrutura;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiSample.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        IMongoDatabase _database;
        IMongoCollection<Feed> _collection;

        int _feedsPerPage = 20;
        int _totalFeeds;
        int _totalPages;

        public NewsController(IMongoDatabase database, IMongoCollection<Feed> collection)
        {
            _database = database;
            _collection = collection;
        }

        private async Task<List<Feed>> GetFeedsAsync(int pageNumber)
        {
            var filter = Builders<Feed>.Filter.Empty;
            CancellationToken cancellationToken = CancellationToken.None;
            var options = new FindOptions<Feed, Feed>
            {
                //Sort = Builders<Feed>.Sort.Ascending(f => f.Title),
                //Skip = (pageNumber - 1) * _feedsPerPage,
                //Limit = _feedsPerPage
            };

            using (var cursor = await _collection.FindAsync(filter, options, cancellationToken))
            {
                var feeds = cursor.Current == null ? await cursor.ToListAsync() : cursor.Current.ToList();
                return feeds;
            }
        }

        [HttpGet("pagenumber/{pagenumber}")]
        public async Task<FeedsResponse> Get(int pagenumber)
        {
            if (pagenumber < 1) pagenumber = 1;

            try
            {
                IEnumerable<Feed> feeds = await GetFeedsAsync(pagenumber);

                _totalFeeds = feeds.Count();

                int lastPage = (_totalFeeds % _feedsPerPage) > 0 ? 1 : 0;

                _totalPages = (_totalFeeds / _feedsPerPage) + lastPage;

                int feedsToSkip = (pagenumber - 1) * _feedsPerPage;

                feeds = feeds
                    .OrderByDescending(feed => feed.PublishDate)
                    .Skip(feedsToSkip)
                    .Take(_feedsPerPage);

                FeedsResponse _feedResponse = new FeedsResponse(totalFeeds: _totalFeeds, totalPages: _totalPages, feedsPerPages: _feedsPerPage, page: pagenumber, feeds: feeds);

                return _feedResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
