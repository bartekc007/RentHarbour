using MediatR;
using Ordering.Application.Domain.Order.Common;
using Ordering.Persistance.Repositories.Mongo;
using Ordering.Persistance.Repositories.Psql;
using RentHarbor.MongoDb.Entities;

namespace Ordering.Application.Domain.Order.GetRentalRequestByUserId
{
    public class GetRentalRequestByUserIdQueryHandler : IRequestHandler<GetRentalRequestByUserIdQuery, GetRentalRequestByUserIdQueryResult>
    {
        private readonly IRentalRequestRepository _rentalRepository;
        private readonly IPropertyRepository _propertyRepository;

        public GetRentalRequestByUserIdQueryHandler(IRentalRequestRepository repository, IPropertyRepository propertyRepository)
        {
            _rentalRepository = repository;
            _propertyRepository = propertyRepository;
        }

        public async Task<GetRentalRequestByUserIdQueryResult> Handle(GetRentalRequestByUserIdQuery request, CancellationToken cancellationToken)
        {
            var rentalRequests = await _rentalRepository.GetRentalRequestsByUserIdAsync(request.UserId);
            List<Property> properties = new List<Property>();
            foreach (var ownerId in rentalRequests.Select(x => x.TenantId).Distinct())
            {
                List<Property> props = await _propertyRepository.GetOwnerPropertiesAsync(ownerId);
                if(props != null)
                   properties.AddRange(props);
            }

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

            return new GetRentalRequestByUserIdQueryResult
            {
                Data = result
            };
        }
    }
}
