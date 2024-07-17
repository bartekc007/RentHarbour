using Catalog.Application.Application.Domains.Property.GetRentedProperites.Dto;
using Catalog.Persistance.Repositories.MongoDb;
using Catalog.Persistance.Repositories.Psql;
using MediatR;

namespace Catalog.Application.Application.Domains.Property.GetRentedProperites
{
    public class GetRentedPropertiesQueryHandler : IRequestHandler<GetRentedPropertiesQuery, GetRentedPropertiesQueryResult>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IRentalRepository _rentalRepository;

        public GetRentedPropertiesQueryHandler(IPropertyRepository propertyRepository, IRentalRepository rentalRepository)
        {
            _propertyRepository = propertyRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task<GetRentedPropertiesQueryResult> Handle(GetRentedPropertiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var rentedProperties = await _rentalRepository.GetByUserIdAsync(request.UserId);

                var properties = new List<RentedProperty>();
                foreach (var rentalRequest in rentedProperties)
                {
                    var property = await _propertyRepository.GetPropertyByIdAsync(rentalRequest.PropertyId);
                    properties.Add(new RentedProperty
                    {
                        PropertyId = property.Id,
                        PropertyName = property.Name,
                        PropertyStreet = property.Address.Street,
                        StartDate = new DateOnly(rentalRequest.StartDate.Year, rentalRequest.StartDate.Month, rentalRequest.StartDate.Day),
                        EndDate = new DateOnly(rentalRequest.EndDate.Year, rentalRequest.EndDate.Month, rentalRequest.EndDate.Day)
                    });
                }

                return new GetRentedPropertiesQueryResult { Data = properties };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
