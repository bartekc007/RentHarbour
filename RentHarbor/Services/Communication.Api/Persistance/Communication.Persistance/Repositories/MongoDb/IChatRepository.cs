using RentHarbor.MongoDb.Entities;

namespace Communication.Persistance.Repositories.MongoDb
{
    public interface IChatRepository
    {
        Task<List<Chat>> GetChatsForUserAsync(string userId);
        Task<Chat> GetChatAsync(string chatId, string userId);
        Task AddChatAsync(Chat chat);
        Task UpdateChatAsync(Chat chat, string userId);
    }
}
