using Document.Persistance.Entities;
using Document.Persistance.Repositories.Psql;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RentalRequestRepository : IRentalRequestRepository
{
    private readonly NpgsqlConnection _connection;

    public RentalRequestRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<RentalRequest> GetRentalRequestByOfferIdAsync(int offerId)
    {
        try
        {
            var query = @"
    SELECT rr.* FROM RentalRequests rr
    WHERE rr.Id = @OfferId";

            var command = new NpgsqlCommand(query, _connection);
            command.Parameters.AddWithValue("@OfferId", offerId);

            await _connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            RentalRequest rentalRequest = null;
            if (await reader.ReadAsync())
            {
                rentalRequest = new RentalRequest
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
                };
            }

            await _connection.CloseAsync();
            return rentalRequest;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
