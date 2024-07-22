using Catalog.Persistance.Entities;

namespace Catalog.Persistance.Repositories.Psql
{
    public interface IRentalRepository
    {
        Task<int> AddAsync(Rental rental);
        Task<bool> ModifyAsync(Rental rental);
        Task<bool> DeleteAsync(string id);
        Task<Rental> GetByIdAsync(string id);
        Task<IEnumerable<Rental>> GetRentalsByUserIdAsync(string userId);
    }
}
