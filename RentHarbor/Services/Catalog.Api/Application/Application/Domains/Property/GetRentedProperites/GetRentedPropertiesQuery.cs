using MediatR;

namespace Catalog.Application.Application.Domains.Property.GetRentedProperites
{
    public class GetRentedPropertiesQuery : IRequest<GetRentedPropertiesQueryResult>
    {
        public string UserId { get; set; }
    }
}
