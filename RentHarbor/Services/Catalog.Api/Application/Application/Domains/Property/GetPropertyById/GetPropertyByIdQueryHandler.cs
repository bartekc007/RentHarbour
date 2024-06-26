﻿using AutoMapper;
using Catalog.Application.Application.Domains.Property.GetPropertyById.Dto;
using MediatR;
using MongoDB.Driver;
using RentHarbor.MongoDb.Data;

namespace Catalog.Application.Application.Domains.Property.GetPropertyById
{
    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, GetPropertyByIdDto>
    {
        private readonly ICatalogContext _context;
        private readonly IMapper _mapper;

        public GetPropertyByIdQueryHandler(ICatalogContext context, IMapper mapper)
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
