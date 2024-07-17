using MongoDB.Driver;
using RentHarbor.MongoDb.Data;
using RentHarbor.MongoDb.Entities;

namespace Communication.Persistance.Repositories.MongoDb
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoContext _context;

        public MessageRepository(IMongoContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Message message)
        {
            await _context.Messages.InsertOneAsync(message);
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(string userId, string chatId)
        {
            var filter = Builders<Message>.Filter.Eq(m => m.ChatId, chatId) &
                         Builders<Message>.Filter.Or(
                             Builders<Message>.Filter.Eq(m => m.SenderId, userId),
                             Builders<Message>.Filter.Eq(m => m.RecipientId, userId));

            return await _context.Messages.Find(filter).ToListAsync();
        }
    }

}
