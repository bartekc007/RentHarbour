using AutoMapper;
using Basket.Application.Domains.Basket.GetFollowedProperties.Dto;
using RentHarbor.MongoDb.Entities;

namespace Basket.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Property, PropertyDto>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ReverseMap();

            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
