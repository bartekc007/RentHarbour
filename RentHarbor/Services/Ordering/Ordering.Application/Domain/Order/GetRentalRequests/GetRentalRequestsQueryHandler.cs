using MediatR;
using Ordering.Application.Domain.Order.GetRentalRequests.Dto;
using Ordering.Persistance.Repositories.Mongo;
using Ordering.Persistance.Repositories.Psql;

namespace Ordering.Application.Domain.Order.GetRentalRequests
{
    public class GetRentalRequestsQueryHandler : IRequestHandler<GetRentalRequestsQuery, GetRentalRequestsQueryResult>
    {
        private readonly IRentalRequestRepository _rentalRepository;
        private readonly IPropertyRepository _propertyRepository;

        public GetRentalRequestsQueryHandler(IRentalRequestRepository repository, IPropertyRepository propertyRepository)
        {
            _rentalRepository = repository;
            _propertyRepository = propertyRepository;
        }

        public async Task<GetRentalRequestsQueryResult> Handle(GetRentalRequestsQuery request, CancellationToken cancellationToken)
        {
            var rentalRequests = await _rentalRepository.GetRentalRequestsByOwnerIdAsync(request.OwnerId);
            var properties = await _propertyRepository.GetOwnerPropertiesAsync(request.OwnerId);
            var result = rentalRequests.Select(rr => {
                var property = properties.FirstOrDefault(p => p.Id == rr.PropertyId);
                return new RentalOffer
                {
                    Id = rr.Id,
                    PropertyId = rr.PropertyId,
                    StartDate = rr.StartDate,
                    EndDate = rr.EndDate,
                    NumberOfPeople = rr.NumberOfPeople,
                    Pets = rr.Pets,
                    MessageToOwner = rr.MessageToOwner,
                    Status = rr.Status,
                    PropertyName = property?.Name,
                    PropertyStreet = property?.Address?.Street,
                    PropertyCity = property?.Address?.City,
                    PropertyState = property?.Address?.State,
                    PropertyPostalCode = property?.Address?.PostalCode,
                    PropertyCountry = property?.Address?.Country,
                    IsAvailable = property?.IsAvailable ?? false,
                    IsActive = property?.IsActive ?? false
                };
            }).ToList();

            return new GetRentalRequestsQueryResult
            {
                Data = result
            };
        }
    }
}
