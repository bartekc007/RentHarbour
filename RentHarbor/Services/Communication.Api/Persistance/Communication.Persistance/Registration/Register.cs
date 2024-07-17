using Communication.Persistance.Context;
using Communication.Persistance.Repositories.MongoDb;
using Communication.Persistance.Repositories.Psql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using RentHarbor.MongoDb.Registration;

namespace Communication.Persistance.Registration
{
    public static class Register
    {
        public static void RegisterPersistanceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterMongoDb(configuration);
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IRentalRequestRepository, RentalRequestRepository>();

            services.AddSingleton<OrderDbContext>();
            services.AddSingleton<NpgsqlConnection>(_ => new NpgsqlConnection(configuration.GetConnectionString("PsqlConnectionString")));
        }
    }
}
