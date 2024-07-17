using MediatR;
using Ordering.Application.Domain.Payment.GetPayments.Dto;

namespace Ordering.Application.Domain.Payment.GetPayments
{
    public class GetPaymentsQuery : IRequest<IEnumerable<PaymentDto>>
    {
        public string UserId { get; set; }
        public string PropertyId { get; set; }
    }
}
