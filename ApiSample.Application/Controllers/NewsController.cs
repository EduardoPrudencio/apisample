using ApiSample.Application.Model;
using ApiSample.Domain;
using ApiSample.Infraestrutura;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ApiSample.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        IMongoDatabase database;
        private MongoDBIntegrate _mongoDBIntegrate;
        IMongoCollection<Feed> collection;

        int _feedsPerPage = 20;
        long _totalFeeds;
        int _totalPages;

        public NewsController()
        {
            _mongoDBIntegrate = new MongoDBIntegrate("mongodb://root:123456@localhost:27017", "mudb");
            database = _mongoDBIntegrate.GetDatabaseConnection();
            collection = database.GetCollection<Feed>("feeds");
        }

        [HttpGet("pagenumber/{pagenumber}")]
        public async Task<FeedsResponse> Get(int pagenumber)
        {
            try
            {
                IFindFluent<Feed, Feed> feedsCollection =  collection.Find(feed => true);

                _totalFeeds = await feedsCollection.CountAsync();
                
                int lastPage = (int)(_totalFeeds % _feedsPerPage) > 0 ? 1 : 0;

                _totalPages = (int)(_totalFeeds / _feedsPerPage) + lastPage;


                int feedsToSkip = (pagenumber - 1) * _feedsPerPage;

                var feedsList = await feedsCollection
                                                .Skip(feedsToSkip)
                                                .Limit(_feedsPerPage)
                                                .ToListAsync();

                FeedsResponse _feedResponse = new FeedsResponse(totalFeeds: _totalFeeds, totalPages: _totalPages, feedsPerPages: _feedsPerPage, page: pagenumber, feeds: feedsList);

                return _feedResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
   
    }
}
