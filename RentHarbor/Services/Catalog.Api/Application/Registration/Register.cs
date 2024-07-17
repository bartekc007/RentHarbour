using Catalog.Application.Application.Domains.Property.GetProperties;
using Catalog.Application.Application.Domains.Property.GetPropertyById.Dto;
using Catalog.Application.Application.Domains.Property.GetPropertyById;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Catalog.Application.Application.Domains.Property.GetRentedProperites;

namespace Application.Registration
{
    public static class Register
    {
        public static void RegisterApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetPropertiesQuery, GetPropertiesResult>, GetPropertiesQueryHandler>();
            services.AddTransient<IRequestHandler<GetPropertyByIdQuery, GetPropertyByIdDto>, GetPropertyByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetRentedPropertiesQuery, GetRentedPropertiesQueryResult>, GetRentedPropertiesQueryHandler>();
        }
    }
}
