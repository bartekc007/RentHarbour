using MediatR;

namespace Basket.Application.Domains.Basket.GetFollowedProperties
{
    public class GetFollowedPropertiesQuery : IRequest<GetFollowedPropertiesResult>
    {
        public string UserId { get; set; }
    }
}
