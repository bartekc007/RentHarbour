using RentHarbor.MongoDb.Entities;

namespace Catalog.Persistance.Repositories.MongoDb
{
    public interface IPropertyRepository
    {
        Task<Property> GetPropertyByIdAsync(string propertyId);
    }
}
