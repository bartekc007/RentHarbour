using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Catalog.Persistance.Context
{
    public class OrderDbContext : DbContext
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
