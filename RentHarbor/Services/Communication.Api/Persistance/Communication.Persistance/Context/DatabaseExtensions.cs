using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Communication.Persistance.Context
{
    public static class DatabaseExtensions
    {
        public static void EnsureDatabaseCreated(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("PsqlConnectionString");

                var sqlFilePath = Path.Combine(AppContext.BaseDirectory, "Context", "create.txt");

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
