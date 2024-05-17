using Basket.Persistance.Repositories;
using MediatR;
using RentHarbor.MongoDb.Entities;

namespace Basket.Application.Domains.Basket.UpdateFollowedProperty
{
    public class UpdateFollowedPropertyCommandHandler : IRequestHandler<UpdateFollowedPropertyCommand, UpdateFollowedPropertyResult>
    {
        private readonly IPropertyRepository _propertyRepository;
        public UpdateFollowedPropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<UpdateFollowedPropertyResult> Handle(UpdateFollowedPropertyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new UpdateFollowedPropertyResult
                {
                    Success = true
                };
                if (request.Action == UserPropertyAction.Add)
                    await AddUserPropertyAsync(request);
                if (request.Action == UserPropertyAction.Remove)
                    response.Success = await RemoveUserPropertyAsync(request.Id, request.UserId);
                return response;
            }
            catch (Exception e)
            {
                return new UpdateFollowedPropertyResult
                {
                    Success = false,
                    ErrorMessage = e.Message
                };
            }
        }

        private async Task<bool> RemoveUserPropertyAsync(string id, string userId)
        {
            return await _propertyRepository.RemoveUserPropertyAsync(id, userId);
        }

        public async Task AddUserPropertyAsync(UpdateFollowedPropertyCommand request)
        {
            var userProperty = new UserProperty
            {
                UserId = request.UserId,
                PropertyId = request.PropertyId,
                AddedAt = DateTime.UtcNow,
            };
            await _propertyRepository.AddUserPropertyAsync(userProperty);
        }
    }
}
