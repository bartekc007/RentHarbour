using Basket.Application.Domains.Basket.UpdateBasket;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application.Registration
{
    public static class Register
    {
        public static void RegisterApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<UpdateBasketCommand, UpdateBasketResult>, UpdateBasketCommandHandler>();
        }
    }
}
