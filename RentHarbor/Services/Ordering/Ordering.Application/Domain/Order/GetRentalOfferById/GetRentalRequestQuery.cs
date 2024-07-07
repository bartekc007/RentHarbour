using MediatR;

namespace Ordering.Application.Domain.Order.GetRentalOfferById
{
    public class GetRentalRequestQuery : IRequest<GetRentalRequestQueryResult>
    {
        public string OwnerId { get; set; }  // User ID of the property owner
        public int OfferId { get; set; }
    }
}
