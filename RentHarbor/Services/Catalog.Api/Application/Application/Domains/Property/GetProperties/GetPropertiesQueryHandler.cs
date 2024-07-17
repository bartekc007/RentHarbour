using AutoMapper;
using Catalog.Application.Application.Domains.Property.GetProperties.Dto;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using RentHarbor.MongoDb.Data;

namespace Catalog.Application.Application.Domains.Property.GetProperties
{
    public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, GetPropertiesResult>
    {
        private readonly IMongoContext _context;
        private readonly IMapper _mapper;
        public GetPropertiesQueryHandler(IMongoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetPropertiesResult> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            var filter = BuildFilters(request);

            // Wykonanie kwerendy z zastosowaniem filtra
            var properties = await _context.Properties.Find(filter).ToListAsync(cancellationToken);
            var propertyDtoList = _mapper.Map<List<PropertyDto>>(properties);

            return new GetPropertiesResult()
            {
                Data = propertyDtoList
            };
        }

        private FilterDefinition<RentHarbor.MongoDb.Entities.Property> BuildFilters(GetPropertiesQuery request)
        {
            // Budowanie filtru kwerendy
            var filterBuilder = Builders<RentHarbor.MongoDb.Entities.Property>.Filter;
            var filter = filterBuilder.Empty; // Zaczynamy od pustego filtru

            if (request.PriceMin.HasValue)
            {
                filter = filter & filterBuilder.Gte(p => p.Price, request.PriceMin.Value);
            }
            if (request.PriceMax.HasValue)
            {
                filter = filter & filterBuilder.Lte(p => p.Price, request.PriceMax.Value);
            }
            if (request.BedroomsMin.HasValue)
            {
                filter = filter & filterBuilder.Gte(p => p.Bedrooms, request.BedroomsMin.Value);
            }
            if (request.BedroomsMax.HasValue)
            {
                filter = filter & filterBuilder.Lte(p => p.Bedrooms, request.BedroomsMax.Value);
            }
            if (request.BathroomsMin.HasValue)
            {
                filter = filter & filterBuilder.Gte(p => p.Bathrooms, request.BathroomsMin.Value);
            }
            if (request.BathroomsMax.HasValue)
            {
                filter = filter & filterBuilder.Lte(p => p.Bathrooms, request.BathroomsMax.Value);
            }
            if (request.AreaSquareMetersMin.HasValue)
            {
                filter = filter & filterBuilder.Gte(p => p.AreaSquareMeters, request.AreaSquareMetersMin.Value);
            }
            if (request.AreaSquareMetersMax.HasValue)
            {
                filter = filter & filterBuilder.Lte(p => p.AreaSquareMeters, request.AreaSquareMetersMax.Value);
            }
            if (!string.IsNullOrEmpty(request.City))
            {
                filter = filter & filterBuilder.Regex(p => p.Address.City, new BsonRegularExpression(request.City, "i"));
            }
            return filter;
        }
    }
}
