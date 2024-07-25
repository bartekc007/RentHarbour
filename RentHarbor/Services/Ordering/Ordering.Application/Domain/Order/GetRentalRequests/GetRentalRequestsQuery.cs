using MediatR;

namespace Ordering.Application.Domain.Order.GetRentalRequests
{
    public class GetRentalRequestsQuery : IRequest<GetRentalRequestsQueryResult>
    {
        public string OwnerId { get; set; }
    }
}
