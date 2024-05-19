using RentHarbor.MongoDb.Entities;

namespace Basket.Persistance.Repositories
{
    public interface IPropertyRepository
    {
        Task AddPropertyAsync(Property property);
        Task<bool> UpdatePropertyAsync(Property property);
        Task<bool> RemovePropertyAsync(string id, string userId);
        Task AddUserPropertyAsync(UserProperty userProperty);
        Task<bool> RemoveUserPropertyAsync(string id, string userId);
        Task<List<UserProperty>> GetUserPropertiesByUserIdAsync(string userId);
        Task<List<Property>> GetPropertiesByIdsAsync(List<string> propertyIds);
    }
}
