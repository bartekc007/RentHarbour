using MediatR;

namespace Ordering.Application.Domain.Order.AcceptRentalRequest
{
    public class AcceptRentalRequestCommand : IRequest<bool>
    {
        public int OfferId { get; set; }
        public string? UserId { get; set; }
        public RentalRequestStatus Status { get; set; }
    }

    public enum RentalRequestStatus
    {
        Accept = 1,
        Modify = 2,
        Close = 3,
    }
}
