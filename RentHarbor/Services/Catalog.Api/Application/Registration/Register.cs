using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Catalog.Application.Domains.Property.GetProperties;
using Catalog.Application.Domains.Property.GetPropertyById;
using Catalog.Application.Domains.Property.GetRentedProperites;
using Catalog.Application.Domains.Property.GetPropertyById.Dto;

namespace Application.Registration
{
    public static class Register
    {
        public static void RegisterApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetPropertiesQuery, GetPropertiesResult>, GetPropertiesQueryHandler>();
            services.AddTransient<IRequestHandler<GetPropertyByIdQuery, GetPropertyByIdDto>, GetPropertyByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetRentedPropertiesQuery, GetRentedPropertiesQueryResult>, GetRentedPropertiesQueryHandler>();

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetPropertiesQueryValidator>());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetPropertyByIdQueryValidator>());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetRentedPropertiesQueryValidator>());
        }
    }
}
