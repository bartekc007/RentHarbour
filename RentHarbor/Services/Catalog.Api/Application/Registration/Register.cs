using Catalog.Application.Application.Domains.Property.GetProperties;
using Catalog.Application.Application.Domains.Property.GetPropertyById.Dto;
using Catalog.Application.Application.Domains.Property.GetPropertyById;
using Catalog.Application.Mapping;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Registration
{
    public static class Register
    {
        public static void RegisterApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetPropertiesQuery, GetPropertiesResult>, GetPropertiesQueryHandler>();
            services.AddTransient<IRequestHandler<GetPropertyByIdQuery, GetPropertyByIdDto>, GetPropertyByIdQueryHandler>();
        }
    }
}
