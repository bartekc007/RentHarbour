using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RentHarbor.MongoDb.Data.ExamplePhotos;
using RentHarbor.MongoDb.Entities;


namespace RentHarbor.MongoDb.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IMongoDatabase _database;
        public CatalogContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDBConnection");
            var databaseName = configuration["MongoDBSettings:DatabaseName"];

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Property> Properties => _database.GetCollection<Property>(CatalogCollections.Properties);

        public IMongoCollection<UserProperty> FollowedProperties => _database.GetCollection<UserProperty>(CatalogCollections.FollowedProperties);
    }
}
