using Catalog.Persistance.Entities;

namespace Catalog.Persistance.Repositories.Psql
{
    public interface IRentalRequestRepository
    {
        Task<List<RentalRequest>> GetRentedPropertiesByUserIdAsync(string userId);
    }
}
