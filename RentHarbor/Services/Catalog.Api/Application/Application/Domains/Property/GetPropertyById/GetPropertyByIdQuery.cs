using Catalog.Application.Application.Domains.Property.GetPropertyById.Dto;
using MediatR;

namespace Catalog.Application.Application.Domains.Property.GetPropertyById
{
    public class GetPropertyByIdQuery : IRequest<GetPropertyByIdDto>
    {
        public string PropertyId { get; }

        public GetPropertyByIdQuery(string propertyId)
        {
            PropertyId = propertyId;
        }
    }
}
