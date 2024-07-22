using AutoMapper;
using Catalog.Application.Domains.Property.GetPropertyById.Dto;
using MediatR;
using MongoDB.Driver;
using RentHarbor.MongoDb.Data;

namespace Catalog.Application.Domains.Property.GetPropertyById
{
    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, GetPropertyByIdDto>
    {
        private readonly IMongoContext _context;
        private readonly IMapper _mapper;

        public GetPropertyByIdQueryHandler(IMongoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetPropertyByIdDto> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _context.Properties.Find(p => p.Id == request.PropertyId).FirstOrDefaultAsync(cancellationToken);

            if (property == null)
                return null;

            return _mapper.Map<GetPropertyByIdDto>(property);
        }
    }
}
