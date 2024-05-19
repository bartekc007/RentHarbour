using MongoDB.Driver;
using RentHarbor.MongoDb.Data;
using RentHarbor.MongoDb.Entities;

namespace Basket.Persistance.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ICatalogContext _context;
        public PropertyRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task AddPropertyAsync(Property property)
        {
            await _context.Properties.InsertOneAsync(property);
        }

        public async Task<bool> UpdatePropertyAsync(Property property)
        {
            var filter = Builders<Property>.Filter.And(
                Builders<Property>.Filter.Eq(p => p.Id, property.Id),
                Builders<Property>.Filter.Eq(p => p.UserId, property.UserId)
            );
            var updateResult = await _context.Properties.ReplaceOneAsync(filter, property);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }


        public async Task<bool> RemovePropertyAsync(string id, string userId)
        {
            var filter = Builders<Property>.Filter.And(
                Builders<Property>.Filter.Eq(p => p.Id, id),
                Builders<Property>.Filter.Eq(p => p.UserId, userId)
            );
            var property = await _context.Properties.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (property != null)
            {
                property.IsActive = false;
                var updateResult = await _context.Properties.ReplaceOneAsync(filter, property);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            return false;
        }

        public async Task AddUserPropertyAsync(UserProperty userProperty)
        {
            await _context.FollowedProperties.InsertOneAsync(userProperty);
        }

        public async Task<bool> RemoveUserPropertyAsync(string id, string userId)
        {
            var filter = Builders<UserProperty>.Filter.And(
                Builders<UserProperty>.Filter.Eq(up => up.Id, id),
                Builders<UserProperty>.Filter.Eq(up => up.UserId, userId)
            );

            var deleteResult = await _context.FollowedProperties.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<List<UserProperty>> GetUserPropertiesByUserIdAsync(string userId)
        {
            var filter = Builders<UserProperty>.Filter.Eq(up => up.UserId, userId);
            return await _context.FollowedProperties.Find(filter).ToListAsync();
        }

        public async Task<List<Property>> GetPropertiesByIdsAsync(List<string> propertyIds)
        {
            var filter = Builders<Property>.Filter.In(p => p.Id, propertyIds);
            return await _context.Properties.Find(filter).ToListAsync();
        }
    }
}
