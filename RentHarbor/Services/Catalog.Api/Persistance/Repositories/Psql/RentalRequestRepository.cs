using Catalog.Persistance.Entities;
using Npgsql;

namespace Catalog.Persistance.Repositories.Psql
{
    public class RentalRequestRepository : IRentalRequestRepository
    {
        private readonly NpgsqlConnection _connection;

        public RentalRequestRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<RentalRequest>> GetRentedPropertiesByUserIdAsync(string userId)
        {
            var query = @"
            SELECT * FROM RentalRequests 
            WHERE (TenantId = @UserId OR UserId = @UserId) AND Status = 'Accepted'";

            var command = new NpgsqlCommand(query, _connection);
            command.Parameters.AddWithValue("@UserId", userId);

            var rentalRequests = new List<RentalRequest>();
            await _connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                rentalRequests.Add(new RentalRequest
                {
                    Id = reader.GetInt32(0),
                    PropertyId = reader.GetString(1),
                    TenantId = reader.GetString(2),
                    UserId = reader.GetString(3),
                    OwnerAcceptance = reader.GetBoolean(4),
                    UserAcceptance = reader.GetBoolean(5),
                    StartDate = reader.GetDateTime(6),
                    EndDate = reader.GetDateTime(7),
                    NumberOfPeople = reader.GetInt32(8),
                    Pets = reader.GetBoolean(9),
                    MessageToOwner = reader.IsDBNull(10) ? null : reader.GetString(10),
                    Status = reader.GetString(11)
                });
            }
            await _connection.CloseAsync();
            return rentalRequests;
        }
    }
}
