using AutoMapper;
using Ordering.Application.Domain.Payment.GetPayments.Dto;
using Ordering.Persistance.Entities;

namespace Ordering.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Payment, PaymentDto>().ReverseMap();
        }
    }
}
