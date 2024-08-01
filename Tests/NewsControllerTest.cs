using Moq;
using MongoDB.Driver;
using ApiSample.Application.Controllers;
using ApiSample.Domain;
using System.Diagnostics;

namespace Tests
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class NewsControllerTest
    {
        //IMongoDatabase mockDatabase;
        NewsController _newsController;

        public NewsControllerTest()
        {
            var mockDatabase = new Mock<IMongoDatabase>();
            var mockCollection = new Mock<IMongoCollection<Feed>>();


            var feeds = new List<Feed>
            {
                new Feed { Id = "1", Title = "Feed 1", Content = "Content 1" },
                new Feed { Id = "2", Title = "Feed 2", Content = "Content 2" }
            };

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
        public void Test1()
        {
            var teste = _newsController.Get(2);
            Assert.True(false);
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}