using Document.Persistance.Entities;
using System.Threading.Tasks;

namespace Document.Persistance.Repositories.Psql
{
    public interface IRentalRequestRepository
    {
        Task<RentalRequest> GetRentalRequestByOfferIdAsync(int offerId);
    }
}
