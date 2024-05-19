using AutoMapper;
using Basket.Persistance.Repositories;
using MediatR;

namespace Basket.Application.Domains.Basket.GetFollowedProperties
{
    public class GetFollowedPropertiesQueryHandler : IRequestHandler<GetFollowedPropertiesQuery, GetFollowedPropertiesResult>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        public GetFollowedPropertiesQueryHandler(IMapper mapper, IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }
        public async Task<GetFollowedPropertiesResult> Handle(GetFollowedPropertiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var properties = await _propertyRepository.GetUserPropertiesByUserIdAsync(request.UserId);
                var userProperties = properties.Select(up => up.PropertyId).ToList();
                var result = await _propertyRepository.GetPropertiesByIdsAsync(userProperties);
                var response = new GetFollowedPropertiesResult
                {
                    Data = _mapper.Map<List<Dto.PropertyDto>>(result)
                };
                response.Success = true;
                return response;
            }
            catch (Exception e)
            {
                return new GetFollowedPropertiesResult
                {
                    Success = false,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}
