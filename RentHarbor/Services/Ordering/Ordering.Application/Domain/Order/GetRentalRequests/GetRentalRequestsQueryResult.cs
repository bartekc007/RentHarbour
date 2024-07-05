using Ordering.Application.Domain.Order.Common;
using Ordering.Persistance.Entities;

namespace Ordering.Application.Domain.Order.GetRentalRequests
{
    public class GetRentalRequestsQueryResult
    {
        public List<RentalOffer> Data { get; set; }
    }
}
