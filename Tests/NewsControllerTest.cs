using Moq;
using MongoDB.Driver;
using ApiSample.Application.Controllers;
using ApiSample.Domain;
using ApiSample.Application.Model;

namespace Tests
{
    public class NewsControllerTest
    {
        NewsController _newsController;

        public NewsControllerTest()
        {
            var mockDatabase   = new Mock<IMongoDatabase>();
            var mockCollection = new Mock<IMongoCollection<Feed>>();
            var mockFindFluenc = new Mock<IFindFluent<Feed, Feed>>();

            var feeds = Configuration.GetFeeds();

            mockDatabase
                .Setup(db => db.GetCollection<Feed>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
                .Returns(mockCollection.Object);

            var mockCursor = new Mock<IAsyncCursor<Feed>>();
            mockCursor.Setup(_ => _.Current).Returns(feeds);
            mockCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            mockCollection
                .Setup(c => c.FindAsync(
                    It.IsAny<FilterDefinition<Feed>>(),
                    It.IsAny<FindOptions<Feed, Feed>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            _newsController = new NewsController(mockDatabase.Object, mockCollection.Object);
        }

        [Fact]
        public void MustReturnFourPages()
        {
            FeedsResponse response = _newsController.Get(1).Result;
            Assert.True(response.TotalPages == 3);
        }

        [Fact]
        public void MustReturnTwentItens()
        {
            FeedsResponse response = _newsController.Get(1).Result;
            Assert.True(response.CurrentCount == 20);
        }
    }
}