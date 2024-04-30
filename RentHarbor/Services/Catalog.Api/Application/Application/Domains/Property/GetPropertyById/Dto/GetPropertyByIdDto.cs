using MongoDB.Bson;
using RentHarbor.MongoDb.Entities;

namespace Catalog.Application.Application.Domains.Property.GetPropertyById.Dto
{
    public class GetPropertyByIdDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public decimal Price { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int AreaSquareMeters { get; set; }
        public bool IsAvailable { get; set; }
        public List<BsonDocument> Photos { get; set; }
    }
}
