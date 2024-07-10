using AutoMapper;
using Document.Application.Domain.Document.Common.Dto;
using RentHarbor.MongoDb.Entities;

namespace Document.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OfferDocument, OfferDocumentDto>().ReverseMap();
        }
    }

}
