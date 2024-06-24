using Ordering.Application.Domain.Order.GetRentalRequests.Dto;
using Ordering.Persistance.Entities;

namespace Ordering.Application.Domain.Order.GetRentalRequests
{
    public class GetRentalRequestsQueryResult
    {
        public List<RentalOffer> Data { get; set; }
    }
}
