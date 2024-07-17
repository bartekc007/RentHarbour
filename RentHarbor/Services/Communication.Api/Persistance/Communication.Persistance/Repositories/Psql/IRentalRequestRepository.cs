using Communication.Persistance.Entities;

namespace Communication.Persistance.Repositories.Psql
{
    public interface IRentalRequestRepository
    {
        Task<RentalRequest> GetRentalRequestByOfferIdAsync(int offerId);
    }
}
