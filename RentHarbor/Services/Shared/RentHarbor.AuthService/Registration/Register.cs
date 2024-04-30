using Microsoft.Extensions.DependencyInjection;
using RentHarbor.AuthService.Services;

namespace RentHarbor.AuthService.Registration
{
    public static class Register
    {
        public static void RegisterAuthService(this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationService, AuthorizationService>();
        }
    }
}
