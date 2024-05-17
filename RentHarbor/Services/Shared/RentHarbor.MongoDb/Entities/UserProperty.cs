using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RentHarbor.MongoDb.Entities
{
    public class UserProperty
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; } 

        public string PropertyId { get; set; } 

        public DateTime AddedAt { get; set; }
    }

}
