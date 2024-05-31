using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using RentHarbor.MongoDb.Entities;
using System.IO;

namespace RentHarbor.MongoDb.Data
{
    public class CatalogContextSeed
    {
        private readonly IMongoCollection<Property> _propertyCollection;

        public CatalogContextSeed(IMongoDatabase database, IConfiguration configuration)
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
            /*var path = Directory.GetCurrentDirectory();
            string photo1Path = "./ExamplePhotos/photo1.jpg";
            string photo2Path = "./ExamplePhotos/photo2.jpg";

            byte[] imageData1 = ReadImageFile(photo1Path);
            byte[] imageData2 = ReadImageFile(photo2Path);*/

            var properties = new List<Property>
            {
                new Property
                {
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
                        /*new BsonDocument
                        {
                            { "Name", "photo1.jpg" },
                            { "Data", new BsonBinaryData(imageData1) },
                            { "ContentType", "image/jpeg" }
                        },
                        new BsonDocument
                        {
                            { "Name", "photo2.jpg" },
                            { "Data", new BsonBinaryData(imageData2) },
                            { "ContentType", "image/jpeg" }
                        }*/
                    }
                },
                new Property
                {
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
                        /*new BsonDocument
                        {
                            { "Name", "photo1.jpg" },
                            { "Data", new BsonBinaryData(imageData1) },
                            { "ContentType", "image/jpeg" }
                        },
                        new BsonDocument
                        {
                            { "Name", "photo2.jpg" },
                            { "Data", new BsonBinaryData(imageData2) },
                            { "ContentType", "image/jpeg" }
                        }*/
                    }
                },
                new Property
                {
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
                        /*new BsonDocument
                        {
                            { "Name", "photo1.jpg" },
                            { "Data", new BsonBinaryData(imageData1) },
                            { "ContentType", "image/jpeg" }
                        },
                        new BsonDocument
                        {
                            { "Name", "photo2.jpg" },
                            { "Data", new BsonBinaryData(imageData2) },
                            { "ContentType", "image/jpeg" }
                        }*/
                    }
                },
                new Property
                {
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
                        /*new BsonDocument
                        {
                            { "Name", "photo1.jpg" },
                            { "Data", new BsonBinaryData(imageData1) },
                            { "ContentType", "image/jpeg" }
                        },
                        new BsonDocument
                        {
                            { "Name", "photo2.jpg" },
                            { "Data", new BsonBinaryData(imageData2) },
                            { "ContentType", "image/jpeg" }
                        }*/
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
