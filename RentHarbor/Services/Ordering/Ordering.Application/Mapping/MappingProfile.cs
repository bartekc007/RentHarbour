using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Mapping
{
    using AutoMapper;
    using Ordering.Application.Domain.Document.Common.Dto;
    using RentHarbor.MongoDb.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OfferDocument, OfferDocumentDto>().ReverseMap();
        }
    }

}
