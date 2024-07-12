using MediatR;

namespace Ordering.Application.Domain.Order.GetRentalRequestByUserId
{
    public class GetRentalRequestByUserIdQuery : IRequest<GetRentalRequestByUserIdQueryResult>
    {
        public string UserId { get; set; }
    }
}
