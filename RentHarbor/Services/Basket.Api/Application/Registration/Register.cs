using Basket.Application.Domains.Basket.GetFollowedProperties;
using Basket.Application.Domains.Basket.UpdateBasket;
using Basket.Application.Domains.Basket.UpdateFollowedProperty;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application.Registration
{
    public static class Register
    {
        public static void RegisterApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<UpdateBasketCommand, UpdateBasketResult>, UpdateBasketCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateFollowedPropertyCommand, UpdateFollowedPropertyResult>, UpdateFollowedPropertyCommandHandler>();
            services.AddTransient<IRequestHandler<GetFollowedPropertiesQuery, GetFollowedPropertiesResult>, GetFollowedPropertiesQueryHandler>();
        }
    }
}
