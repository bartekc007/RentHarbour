using Authorization.Application.Domains.User.Login;
using Authorization.Application.Domains.User.RefreshToken;
using Authorization.Application.Domains.User.Register;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

namespace Authorization.Application.Registration
{
    public static class Register
    {
        public static void RegisterApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<RegisterCommand, RegisterResult>, RegisterCommandHandler>();
            services.AddTransient<IRequestHandler<RefreshTokenCommand, RefreshTokenResult>, RefreshTokenCommandHandler>();
            services.AddTransient<IRequestHandler<LoginCommand, LoginResult>, LoginCommandHandler>();

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RefreshTokenCommandValidator>());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginCommandValidator>());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterCommandValidator>());

        }
    }
}
