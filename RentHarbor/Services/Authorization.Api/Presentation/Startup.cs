using Authorization.Api.Healthchecks;
using Authorization.Application.Registration;
using Authorization.Infrastructure.Registration;
using Authorization.Persistance.Context;
using Authorization.Persistance.Entities;
using Authorization.Persistance.Extentions;
using Authorization.Persistance.Registration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Authorization.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterPersistanceLayer(Configuration);
            services.RegisterInfrastructureLayer(Configuration);
            services.RegisterApplicationLayer();

            services.AddMediatR(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Authorization.Api", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            services.AddHealthChecks()
                .AddCheck<ReadinessHealthCheck>("readiness")
                .AddCheck<LivenessHealthCheck>("liveness");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authorization.Api v1"));
            }

            if (env.IsDevelopment())
            {
                using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetService<AuthDbContext>();
                    app.InitialiseDatabaseAsync(dbContext).Wait();

                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    app.SeedDatabaseAsync(dbContext, userManager).Wait();
                }
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health/readiness");
                endpoints.MapHealthChecks("/health/live");
            });
        }
    }
}
