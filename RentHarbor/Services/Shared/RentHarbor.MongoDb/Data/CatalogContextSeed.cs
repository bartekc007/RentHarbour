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
                    Description = "Beautiful villa with a large garden",
                    Address = new Address
                    {
                        Street = "123 Main Street",
                        City = "New York",
                        State = "NY",
                        PostalCode = "10001",
                        Country = "USA"
                    },
                    Price = 1000000,
                    Bedrooms = 5,
                    Bathrooms = 3,
                    AreaSquareMeters = 300,
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
                // Add more properties as needed
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
