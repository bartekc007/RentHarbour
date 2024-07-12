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

        public AcceptRentalRequestCommandHandler(IRentalRequestRepository rentalRequestRepository, IRentalRepository rentalRepository, IPropertyRepository propertyRepository)
        {
            _rentalRequestRepository = rentalRequestRepository;
            _rentalRepository = rentalRepository;
            _propertyRepository = propertyRepository;
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
                Id = Guid.NewGuid().ToString(),  // Assuming the Id is a string. Change as necessary.
                PropertyId = rentalOffer.PropertyId,
                RentalRequestId = rentalOffer.Id.ToString(),
                RenterId = rentalOffer.UserId
            };

            await _rentalRepository.AddAsync(rental);

            // Update the property to indicate it is rented
            var property = await _propertyRepository.GetOwnerPropertyAsync(rentalOffer.TenantId, rentalOffer.PropertyId);
            if (property != null)
            {
                property.RentedByUserId = rentalOffer.UserId;
                property.IsAvailable = false;

                // Assuming there is a method to update the property
                // Update the property in the repository. 
                // Example: await _propertyRepository.UpdateAsync(property);
                await _propertyRepository.UpdateAsync(property);
            }
        }
    }
}
