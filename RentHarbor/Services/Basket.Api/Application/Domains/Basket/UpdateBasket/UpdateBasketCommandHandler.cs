using Basket.Persistance.Repositories;
using MediatR;
using RentHarbor.MongoDb.Entities;

namespace Basket.Application.Domains.Basket.UpdateBasket
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, UpdateBasketResult>
    {
        private readonly IPropertyRepository _propertyRepository;
        public UpdateBasketCommandHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<UpdateBasketResult> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new UpdateBasketResult()
                {
                    Success = true
                };

                if (request.Action == BasketAction.Add)
                    await AddProperty(request);

                if (request.Action == BasketAction.Update)
                    response.Success = await UpdateProperty(request);

                if (request.Action == BasketAction.Remove)
                    response.Success = await RemoveProperty(request);

                return response;
            }
            catch (Exception e)
            {
                return new UpdateBasketResult
                {
                    Success = false,
                    ErrorMessage = e.Message
                };
            }
        }

        private async Task AddProperty(UpdateBasketCommand request)
        {
            var property = new Property
            {
                UserId = request.UserId,
                Name = request.Name,
                Description = request.Description,
                Address = new Address
                {
                    Street = request.AddressStreet,
                    City = request.AddressCity,
                    State = request.AddressState,
                    PostalCode = request.AddressPostalCode,
                    Country = request.AddressCountry
                },
                Price = request.Price,
                Bedrooms = request.Bedrooms,
                Bathrooms = request.Bathrooms,
                AreaSquareMeters = request.AreaSquareMeters,
                IsAvailable = request.IsAvailable,
                IsActive = true
            };
            await _propertyRepository.AddPropertyAsync(property);
        }

        private async Task<bool> RemoveProperty(UpdateBasketCommand request)
        {
            return await _propertyRepository.RemovePropertyAsync(request.PropertyId, request.UserId);
        }

        private async Task<bool> UpdateProperty(UpdateBasketCommand request)
        {
            var property = new Property
            {
                UserId = request.UserId,
                Id = request.PropertyId,
                Name = request.Name,
                Description = request.Description,
                Address = new Address
                {
                    Street = request.AddressStreet,
                    City = request.AddressCity,
                    State = request.AddressState,
                    PostalCode = request.AddressPostalCode,
                    Country = request.AddressCountry
                },
                Price = request.Price,
                Bedrooms = request.Bedrooms,
                Bathrooms = request.Bathrooms,
                AreaSquareMeters = request.AreaSquareMeters,
                IsAvailable = request.IsAvailable,
                IsActive = true
            };
            return await _propertyRepository.UpdatePropertyAsync(property);
        }
    }
}
