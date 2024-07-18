using ApiSample.Domain;
using ApiSample.Infraestrutura;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiSample.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        IMongoDatabase database;
        private MongoDBIntegrate _mongoDBIntegrate;
        IMongoCollection<Feed> collection;

        public NewsController()
        {
            _mongoDBIntegrate = new MongoDBIntegrate("mongodb://root:123456@localhost:27017", "mudb");
            database = _mongoDBIntegrate.GetDatabaseConnection();
            collection = database.GetCollection<Feed>("feeds");
        }

        [HttpGet("pagenumber/{pagenumber}/pagesize/{pagesize}")]
        public async Task<IEnumerable<Feed>> Get(int pagenumber, int pagesize)
        {
            try
            {

                var feedsList = await collection.Find(feed => true)
                                                .Skip(pagenumber)
                                                .Limit(pagesize)
                                                .ToListAsync();

                return feedsList;
            }
            catch (Exception)
            {
                throw;
            }
        }
   
    }
}
