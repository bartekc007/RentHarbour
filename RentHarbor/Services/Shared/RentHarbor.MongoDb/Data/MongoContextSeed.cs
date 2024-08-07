﻿using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using RentHarbor.MongoDb.Entities;

namespace RentHarbor.MongoDb.Data
{
    public class MongoContextSeed
    {
        private readonly IMongoCollection<Property> _propertyCollection;

        public MongoContextSeed(IMongoDatabase database, IConfiguration configuration)
        {
            _propertyCollection = database.GetCollection<Property>(configuration.GetSection("MongoDBSettings:CollectionName").Value);
        }

        public void Seed()
        {
            if (!_propertyCollection.Find(_ => true).Any())
            {
                var properties = GetSeedData();
                _propertyCollection.InsertMany(properties);
            }
        }

        private List<Property> GetSeedData()
        {
            var properties = new List<Property>
            {
                new Property
                {
                    UserId = "6151b2c2-f9bb-4a91-8b74-2ee834090da3",
                    Name = "Spacious Villa",
                    Description = "Beautiful Large villa with a large garden",
                    Address = new Address
                    {
                        Street = "123 Main Street",
                        City = "New York",
                        State = "NY",
                        PostalCode = "10001",
                        Country = "USA"
                    },
                    Price = 1100,
                    Bedrooms = 5,
                    Bathrooms = 3,
                    AreaSquareMeters = 130,
                    IsAvailable = true,
                    IsActive = true,
                    Photos = new List<BsonDocument>
                    {

                    }
                },
                new Property
                {
                    UserId = "6151b2c2-f9bb-4a91-8b74-2ee834090da3",
                    Name = "Medium Villa",
                    Description = "Beautiful villa with a medium garden",
                    Address = new Address
                    {
                        Street = "123 Main Street",
                        City = "Warszawa",
                        State = "Warszawa",
                        PostalCode = "10001",
                        Country = "Polska"
                    },
                    Price = 1000,
                    Bedrooms = 3,
                    Bathrooms = 1,
                    AreaSquareMeters = 66,
                    IsAvailable = true,
                    IsActive = true,
                    Photos = new List<BsonDocument>
                    {

                    }
                },
                new Property
                {
                    UserId = "6151b2c2-f9bb-4a91-8b74-2ee834090da3",
                    Name = "Smal Apratment",
                    Description = "Beautiful small apartment without garden",
                    Address = new Address
                    {
                        Street = "117 Main Street",
                        City = "Kraków",
                        State = "Kraków",
                        PostalCode = "10001",
                        Country = "Polska"
                    },
                    Price = 900,
                    Bedrooms = 2,
                    Bathrooms = 1,
                    AreaSquareMeters = 45,
                    IsAvailable = true,
                    IsActive = true,
                    Photos = new List<BsonDocument>
                    {

                    }
                },
                new Property
                {
                    UserId = "6151b2c2-f9bb-4a91-8b74-2ee834090da3",
                    Name = "Medium Villa",
                    Description = "Beautiful villa with a medium garden",
                    Address = new Address
                    {
                        Street = "125 Main Street",
                        City = "Kraków",
                        State = "Kraków",
                        PostalCode = "10001",
                        Country = "Polska"
                    },
                    Price = 1000,
                    Bedrooms = 3,
                    Bathrooms = 1,
                    AreaSquareMeters = 66,
                    IsAvailable = true,
                    IsActive = true,
                    Photos = new List<BsonDocument>
                    {

                    }
                },
            };

            return properties;
        }

        byte[] ReadImageFile(string path)
        {
            byte[] imageData;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                imageData = new byte[fs.Length];
                fs.Read(imageData, 0, (int)fs.Length);
            }
            return imageData;
        }
    }
}
