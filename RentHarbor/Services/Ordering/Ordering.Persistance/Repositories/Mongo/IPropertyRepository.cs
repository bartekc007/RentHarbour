using RentHarbor.MongoDb.Entities;

namespace Ordering.Persistance.Repositories.Mongo
{
    public interface IPropertyRepository
    {
        Task<string> GetOwnerIdByPropertyIdAsync(string propertyId);
        Task<List<Property>> GetOwnerPropertiesAsync(string ownerId);
    }
}
