using MongoDB.Driver;
using RentHarbor.MongoDb.Entities;

namespace Communication.Persistance.Repositories.MongoDb
{
    public class ChatRepository : IChatRepository
    {
        private readonly IMongoCollection<Chat> _chatCollection;

        public ChatRepository(IMongoDatabase database)
        {
            _chatCollection = database.GetCollection<Chat>("Chats");
        }

        public async Task<List<Chat>> GetChatsForUserAsync(string userId)
        {
            var filter = Builders<Chat>.Filter.Where(c => c.User1Id == userId || c.User2Id == userId);
            return await _chatCollection.Find(filter).ToListAsync();
        }

        public async Task<Chat> GetChatAsync(string chatId, string userId)
        {
            var filter = Builders<Chat>.Filter.Where(c => c.Id == chatId && ( c.User1Id == userId || c.User2Id == userId));
            return await _chatCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddChatAsync(Chat chat)
        {
            await _chatCollection.InsertOneAsync(chat);
        }

        public async Task UpdateChatAsync(Chat chat, string userId)
        {
            var filter = Builders<Chat>.Filter.Where(c => c.Id == chat.Id && (c.User1Id == userId || c.User2Id == userId));
            await _chatCollection.ReplaceOneAsync(filter, chat);
        }
    }
}
