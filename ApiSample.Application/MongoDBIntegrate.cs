using MongoDB.Driver;

namespace ApiSample.Application;

public interface IMongoDBIntegrate
{
    IMongoDatabase GetDatabaseConnection();
}

public class MongoDBIntegrate : IMongoDBIntegrate
{
    private readonly string _connectionString;
    private readonly string _databaseName;
    
    public MongoDBIntegrate(string connectionString, string databaseName)
    {
        _connectionString = connectionString;
        _databaseName = databaseName;
    }

    public IMongoDatabase GetDatabaseConnection()
    {
        var client = new MongoClient(_connectionString);
        return client.GetDatabase(_databaseName);
    }
}
