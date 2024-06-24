using MediatR;

namespace Ordering.Application.Domain.Order.RentProperty
{
    public class RentPropertyCommand : IRequest<RentPropertyResult>
    {
        public Guid UserId { get; set; }
        public string PropertyId { get; set; }
    }
}
