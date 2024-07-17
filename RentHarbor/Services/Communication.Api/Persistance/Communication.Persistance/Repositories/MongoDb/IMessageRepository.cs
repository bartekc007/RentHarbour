using RentHarbor.MongoDb.Entities;

namespace Communication.Persistance.Repositories.MongoDb
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message);
        Task<IEnumerable<Message>> GetMessagesAsync(string userId, string chatId);
    }

}
