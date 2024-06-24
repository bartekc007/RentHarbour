using MediatR;

namespace Ordering.Application.Domain.Order.CreateRentalRequest
{
    public class CreateRentalRequestCommand : IRequest<CreateRentalRequestCommandResult>
    {
        public string PropertyId { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPeople { get; set; }
        public bool Pets { get; set; }
        public string MessageToOwner { get; set; }
    }
}
