using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Communication.Persistance.Context
{
    public class OrderDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public OrderDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("PsqlConnectionString");
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
