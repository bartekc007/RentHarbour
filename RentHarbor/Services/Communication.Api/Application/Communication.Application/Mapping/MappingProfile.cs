using AutoMapper;
using Communication.Application.Domain.Message.Common.Dto;
using RentHarbor.MongoDb.Entities;

namespace Communication.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessageDto>().ReverseMap();
        }
    }

}
