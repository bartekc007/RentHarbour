using AutoMapper;
using MediatR;
using Ordering.Application.Domain.Payment.GetPayments.Dto;
using Ordering.Persistance.Repositories.Psql;

namespace Ordering.Application.Domain.Payment.GetPayments
{
    public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, IEnumerable<PaymentDto>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public GetPaymentsQueryHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentDto>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentRepository.GetByUserIdAndPropertyIdAsync(request.UserId, request.PropertyId);
            return _mapper.Map<List<PaymentDto>>(payments);
        }
    }
}
