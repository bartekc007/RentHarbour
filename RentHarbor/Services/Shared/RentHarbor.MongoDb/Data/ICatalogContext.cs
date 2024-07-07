using MongoDB.Driver;
using RentHarbor.MongoDb.Entities;

namespace RentHarbor.MongoDb.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Property> Properties { get; }
        IMongoCollection<OfferDocument> OfferDocuments { get; }
        IMongoCollection<UserProperty> FollowedProperties { get; }
    }
}
