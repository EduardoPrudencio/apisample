using System;
using MongoDB.Driver;

namespace ApiSample.Infraestrutura
{
    public class MongoDBIntegrate
    {
        //IMongoDatabase? database;

        public MongoDBIntegrate(string connectionString, string dataBaseName)
        {
            ConnectionString = connectionString;
            DataBaseName = dataBaseName;
        }

        public string ConnectionString { get; set; }
        public string DataBaseName { get; set; }

        public IMongoDatabase GetDatabaseConnection()
        {
            var client = new MongoClient(ConnectionString);
            return client.GetDatabase("mudb");
        }
    }
}
