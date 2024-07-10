using Document.Persistance.Context;
using Document.Persistance.Repositories.Mongo;
using Document.Persistance.Repositories.Psql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using RentHarbor.MongoDb.Registration;

namespace Document.Persistance.Registration
{
    public static class Register
    {
        public static void RegisterPersistanceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterMongoDb(configuration);
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<IRentalRequestRepository, RentalRequestRepository>();

            services.AddSingleton<OrderDbContext>();
            services.AddSingleton<NpgsqlConnection>(_ => new NpgsqlConnection(configuration.GetConnectionString("PsqlConnectionString")));
        }
    }
}
