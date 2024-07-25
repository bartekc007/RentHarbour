using Ordering.Persistance.Entities;

namespace Ordering.Persistance.Repositories.Psql
{
    public interface IRentalRequestRepository
    {
        Task<int> AddAsync(RentalRequest rentalRequest);
        Task<List<RentalRequest>> GetRentalRequestsByOwnerIdAsync(string ownerId);
        Task<List<RentalRequest>> GetRentalRequestsByUserIdAsync(string ownerId);
        Task<RentalRequest?> GetRentalRequestByOwnerIdAndOfferIdAsync(string ownerId, int offerId);
        Task<RentalRequest> GetRentalRequestByOfferIdAsync( int offerId);
        Task<bool> ModifyAsync(RentalRequest rentalRequest);
        Task<bool> DeleteAsync(int id);
    }
}
