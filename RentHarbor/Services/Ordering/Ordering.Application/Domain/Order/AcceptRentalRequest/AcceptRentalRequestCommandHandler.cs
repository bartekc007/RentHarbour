using MediatR;
using Ordering.Persistance.Entities;
using Ordering.Persistance.Repositories.Mongo;
using Ordering.Persistance.Repositories.Psql;

namespace Ordering.Application.Domain.Order.AcceptRentalRequest
{
    public class AcceptRentalRequestCommandHandler : IRequestHandler<AcceptRentalRequestCommand, bool>
    {
        private readonly IRentalRequestRepository _rentalRequestRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPaymentRepository _paymentRepository;

        public AcceptRentalRequestCommandHandler(IRentalRequestRepository rentalRequestRepository,
            IRentalRepository rentalRepository,
            IPropertyRepository propertyRepository,
            IPaymentRepository paymentRepository)
        {
            _rentalRequestRepository = rentalRequestRepository;
            _rentalRepository = rentalRepository;
            _propertyRepository = propertyRepository;
            _paymentRepository = paymentRepository;
        }
        public async Task<bool> Handle(AcceptRentalRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rentalOffer = await _rentalRequestRepository.GetRentalRequestByOfferIdAsync(request.OfferId);

                if (request.Status == RentalRequestStatus.Accept)
                {
                    if (rentalOffer.TenantId == request.UserId)
                        rentalOffer.OwnerAcceptance = true;
                    else
                        rentalOffer.UserAcceptance = true;

                    if (rentalOffer.OwnerAcceptance && rentalOffer.UserAcceptance)
                    {
                        await AcceptRental(rentalOffer);
                    }

                    await _rentalRequestRepository.ModifyAsync(rentalOffer);
                }

                if (request.Status == RentalRequestStatus.Modify)
                {
                    if (rentalOffer.TenantId == request.UserId)
                        rentalOffer.UserAcceptance = false;
                    else
                        rentalOffer.OwnerAcceptance = false;

                    await _rentalRequestRepository.ModifyAsync(rentalOffer);
                }

                if (request.Status == RentalRequestStatus.Close)
                {
                    await _rentalRequestRepository.DeleteAsync(rentalOffer.Id);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        private async Task AcceptRental(RentalRequest rentalOffer)
        {
            // Create a new rental
            var rental = new Rental
            {
                Id = Guid.NewGuid().ToString(),
                PropertyId = rentalOffer.PropertyId,
                RentalRequestId = rentalOffer.Id.ToString(),
                RenterId = rentalOffer.UserId,
                StartDate = new DateOnly(rentalOffer.StartDate.Year, rentalOffer.StartDate.Month, rentalOffer.StartDate.Day),
                EndDate = new DateOnly(rentalOffer.EndDate.Year, rentalOffer.EndDate.Month, rentalOffer.EndDate.Day)
            };

            await _rentalRepository.AddAsync(rental);

            var property = await _propertyRepository.GetOwnerPropertyAsync(rentalOffer.TenantId, rentalOffer.PropertyId);
            if (property != null)
            {
                property.RentedByUserId = rentalOffer.UserId;
                property.IsAvailable = false;
                await _propertyRepository.UpdateAsync(property);
            }

            await GeneratePaymentsFromRentalRequest(rentalOffer);
        }

        private async Task GeneratePaymentsFromRentalRequest(RentalRequest rentalRequest)
        {
            DateTime startDate = rentalRequest.StartDate;
            DateTime endDate = rentalRequest.EndDate;

            List<Persistance.Entities.Payment> payments = GeneratePayments(startDate, endDate);

            foreach (var payment in payments)
            {
                payment.UserId = rentalRequest.UserId;
                payment.PropertyId = rentalRequest.PropertyId;
                payment.IsPaid = false; // Initial state
                payment.PaidDate = null; // Initial state

                await _paymentRepository.AddAsync(payment);
            }
        }

        private List<Persistance.Entities.Payment> GeneratePayments(DateTime startDate, DateTime endDate)
        {
            List<Persistance.Entities.Payment> payments = new List<Persistance.Entities.Payment>();

            DateTime currentDate = startDate;
            DateTime lastDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));

            while (currentDate <= endDate)
            {
                Persistance.Entities.Payment payment = new Persistance.Entities.Payment
                {
                    PaymentDate = lastDayOfMonth,
                    Amount = CalculatePaymentAmount(startDate, endDate),
                    IsPaid = false, // Initial state
                    PaidDate = null // Initial state
                };

                payments.Add(payment);

                // Move to the next month
                currentDate = lastDayOfMonth.AddMonths(1);
                lastDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
            }

            return payments;
        }

        private decimal CalculatePaymentAmount(DateTime startDate, DateTime endDate)
        {
            // Example calculation, adjust as per your business logic
            int totalMonths = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month + 1;
            decimal monthlyPayment = 1000; // Example amount per month
            decimal totalAmount = totalMonths * monthlyPayment;
            return totalAmount;
        }
    }
}
