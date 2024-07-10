using RentHarbor.MongoDb.Entities;

namespace Ordering.Persistance.Repositories.Mongo
{
    public interface IPropertyRepository
    {
        Task<string> GetOwnerIdByPropertyIdAsync(string propertyId);
        Task<List<Property>> GetOwnerPropertiesAsync(string ownerId);
        Task<Property> GetOwnerPropertyAsync(string ownerId, string propertyId);
        Task<bool> UpdateAsync( Property property);
    }
}
