using Communication.Api.Healthchecks;
using Communication.Api.Hubs;
using Communication.Application.Mapping;
using Communication.Application.Registration;
using Communication.Persistance.Registration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RentHarbor.AuthService.Registration;

namespace Communication.Api
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
            services.RegisterApplicationLayer();
            services.RegisterAuthService();

            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddSignalR();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Communication.Api", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });
            });
            services.AddHealthChecks()
                .AddCheck<ReadinessHealthCheck>("readiness")
                .AddCheck<LivenessHealthCheck>("liveness");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowSpecificOrigins");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Communication.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<ChatHub>("/chatHub");
                endpoints.MapHealthChecks("/health/readiness");
                endpoints.MapHealthChecks("/health/live");
            });
        }
    }
}
