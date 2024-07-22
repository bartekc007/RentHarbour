using Catalog.Persistance.Repositories.MongoDb;
using Catalog.Persistance.Repositories.Psql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;


namespace Catalog.Persistance.Context
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PsqlConnectionString");

            services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(connectionString));

            services.AddDbContext<OrderDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IRentalRequestRepository, RentalRequestRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();

            return services;
        }

        public static void EnsureDatabaseCreated(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("PsqlConnectionString");

                // Ścieżka do pliku SQL
                var sqlFilePath = Path.Combine(AppContext.BaseDirectory, "Context", "create.txt");

                // Odczytanie zawartości pliku SQL
                var commandText = File.ReadAllText(sqlFilePath);

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(commandText, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
        }
    }

}
