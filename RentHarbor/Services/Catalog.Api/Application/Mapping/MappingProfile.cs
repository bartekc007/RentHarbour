using AutoMapper;
using Catalog.Application.Application.Domains.Property.GetProperties.Dto;
using Catalog.Application.Application.Domains.Property.GetPropertyById.Dto;
using RentHarbor.MongoDb.Entities;

namespace Catalog.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Property, PropertyDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ReverseMap();

            CreateMap<Address, AddressDto>()
                .ReverseMap();

            CreateMap<Property, GetPropertyByIdDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ReverseMap();
        }
    }
}
