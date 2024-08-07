﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RentHarbor.MongoDb.Entities;


namespace RentHarbor.MongoDb.Data
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database;
        public MongoContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDBConnection");
            var databaseName = configuration["MongoDBSettings:DatabaseName"];

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Property> Properties => _database.GetCollection<Property>(CatalogCollections.Properties);
        public IMongoCollection<OfferDocument> OfferDocuments => _database.GetCollection<OfferDocument>(CatalogCollections.OfferDocuments);
        public IMongoCollection<UserProperty> FollowedProperties => _database.GetCollection<UserProperty>(CatalogCollections.FollowedProperties);
        public IMongoCollection<Message> Messages => _database.GetCollection<Message>(CatalogCollections.Messages);
    }
}
