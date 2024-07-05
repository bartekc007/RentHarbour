using Ordering.Persistance.Entities;

namespace Ordering.Persistance.Repositories.Psql
{
    public interface IRentalRequestRepository
    {
        Task<int> AddAsync(RentalRequest rentalRequest);
        Task<List<RentalRequest>> GetRentalRequestsByOwnerIdAsync(string ownerId);
        Task<RentalRequest> GetRentalRequestByOwnerIdAsync(string ownerId, string offerId);
    }
}
