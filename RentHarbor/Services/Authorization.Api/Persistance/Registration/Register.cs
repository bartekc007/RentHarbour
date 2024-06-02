using Authorization.Persistance.Context;
using Authorization.Persistance.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Persistance.Registration
{
    public static class Register
    {
        public static void RegisterPersistanceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Auth_MSSQL")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.AllowedForNewUsers = true;
            })
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
