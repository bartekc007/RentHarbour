using MongoDB.Driver;
using RentHarbor.MongoDb.Data;
using RentHarbor.MongoDb.Entities;

namespace Ordering.Persistance.Repositories.Mongo
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ICatalogContext _context;
        public PropertyRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<string> GetOwnerIdByPropertyIdAsync(string propertyId)
        {
            var filter = Builders<Property>.Filter.Eq(p => p.Id, propertyId);
            var property = await _context.Properties.Find(filter).FirstOrDefaultAsync();

            return property?.UserId;
        }

        public async Task<List<Property>> GetOwnerPropertiesAsync(string ownerId)
        {
            var filter = Builders<Property>.Filter.Eq(p => p.UserId, ownerId);
            var properties = await _context.Properties.Find(filter).ToListAsync();
            return properties;
        }
    }
}
