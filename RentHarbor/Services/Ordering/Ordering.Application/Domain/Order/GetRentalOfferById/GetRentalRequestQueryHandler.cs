using MediatR;
using Ordering.Application.Domain.Order.Common;
using Ordering.Persistance.Repositories.Mongo;
using Ordering.Persistance.Repositories.Psql;

namespace Ordering.Application.Domain.Order.GetRentalOfferById
{
    public class GetRentalRequestQueryHandler : IRequestHandler<GetRentalRequestQuery, GetRentalRequestQueryResult>
    {
        private readonly IRentalRequestRepository _rentalRepository;
        private readonly IPropertyRepository _propertyRepository;

        public GetRentalRequestQueryHandler(IRentalRequestRepository repository, IPropertyRepository propertyRepository)
        {
            _rentalRepository = repository;
            _propertyRepository = propertyRepository;
        }

        public async Task<GetRentalRequestQueryResult> Handle(GetRentalRequestQuery request, CancellationToken cancellationToken)
        {
            // Pobierz pojedynczą ofertę wynajmu na podstawie ownerId i offerId
            var rentalRequest = await _rentalRepository.GetRentalRequestByOwnerIdAndOfferIdAsync(request.OwnerId, request.OfferId);

            // Sprawdź, czy oferta wynajmu została znaleziona
            if (rentalRequest == null)
            {
                return new GetRentalRequestQueryResult
                {
                    Data = null
                };
            }

            var property = await _propertyRepository.GetOwnerPropertyAsync(request.OwnerId, rentalRequest.PropertyId);

            var result = new RentalOffer
            {
                Id = rentalRequest.Id,
                PropertyId = rentalRequest.PropertyId,
                StartDate = rentalRequest.StartDate,
                EndDate = rentalRequest.EndDate,
                NumberOfPeople = rentalRequest.NumberOfPeople,
                Pets = rentalRequest.Pets,
                MessageToOwner = rentalRequest.MessageToOwner,
                Status = rentalRequest.Status,
                PropertyName = property?.Name,
                PropertyStreet = property?.Address?.Street,
                PropertyCity = property?.Address?.City,
                PropertyState = property?.Address?.State,
                PropertyPostalCode = property?.Address?.PostalCode,
                PropertyCountry = property?.Address?.Country,
                IsAvailable = property?.IsAvailable ?? false,
                IsActive = property?.IsActive ?? false
            };

            return new GetRentalRequestQueryResult
            {
                Data = result
            };
        }

    }
}
