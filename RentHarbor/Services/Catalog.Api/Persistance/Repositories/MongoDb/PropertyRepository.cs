using MongoDB.Driver;
using RentHarbor.MongoDb.Data;
using RentHarbor.MongoDb.Entities;

namespace Catalog.Persistance.Repositories.MongoDb
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IMongoContext _context;

        public PropertyRepository(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Property> GetPropertyByIdAsync(string propertyId)
        {
            var filter = Builders<Property>.Filter.Eq(p => p.Id, propertyId);
            return await _context.Properties.Find(filter).FirstOrDefaultAsync();
        }
    }
}
