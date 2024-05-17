using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RentHarbor.MongoDb.Entities
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public decimal Price { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int AreaSquareMeters { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }
        public List<BsonDocument> Photos { get; set; }
        public Property()
        {
            Photos = new List<BsonDocument>();
        }
    }
}
