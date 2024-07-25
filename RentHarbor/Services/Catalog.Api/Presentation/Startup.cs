using Application.Registration;
using Catalog.Api.Healthchecks;
using Catalog.Application.Extensions;
using Catalog.Application.Mapping;
using Catalog.Persistance.Registration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RentHarbor.AuthService.Registration;
using RentHarbor.MongoDb.Data;

namespace Catalog.Api
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
            services.AddAutoMapper(typeof(Startup), typeof(MappingProfile));

            services.AddMediatR(typeof(Startup));
            services.AddControllers()
                .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                    });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.Api", Version = "v1" });
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MongoContextSeed catalogContextSeed)
        {
            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.Api v1"));

                catalogContextSeed.Seed();
            }

            app.UseRouting();

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
