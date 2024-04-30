using AutoMapper;
using Catalog.Application.Application.Domains.Property.GetProperties.Dto;
using MediatR;
using MongoDB.Driver;
using RentHarbor.MongoDb.Data;

namespace Catalog.Application.Application.Domains.Property.GetProperties
{
    public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, GetPropertiesResult>
    {
        private readonly ICatalogContext _context;
        private readonly IMapper _mapper;
        public GetPropertiesQueryHandler(ICatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetPropertiesResult> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await _context.Properties.Find(_ => true).ToListAsync();
            var propertyDtoList = _mapper.Map<List<PropertyDto>>(properties);
            return new GetPropertiesResult()
            {
                Data = propertyDtoList
            };
        }
    }
}
