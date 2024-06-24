using Ordering.Persistance.Repositories.Mongo;
using MediatR;

namespace Ordering.Application.Domain.Order.RentProperty
{
    /*public class RentPropertyCommandHandler : IRequestHandler<RentPropertyCommand,RentPropertyResult>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserPropertyRepository _userPropertyRepository;

        public RentPropertyCommandHandler(IPropertyRepository propertyRepository, IUserPropertyRepository userPropertyRepository)
        {
            _propertyRepository = propertyRepository;
            _userPropertyRepository = userPropertyRepository;
        }

        public async Task<RentPropertyResult> Handle(RentPropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetPropertyByIdAsync(request.PropertyId);

            if (property == null || !property.IsAvailable)
            {
                throw new Exception("Property is not available.");
            }

            var userProperty = new UserProperty
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                PropertyId = request.PropertyId,
                AddedAt = DateTime.UtcNow
            };

            var result = await _userPropertyRepository.AddUserPropertyAsync(userProperty);

            return new RentPropertyResult();
        }
    }*/
}
