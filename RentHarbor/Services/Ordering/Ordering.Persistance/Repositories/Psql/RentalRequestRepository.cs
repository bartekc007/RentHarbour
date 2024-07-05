using Npgsql;
using Ordering.Persistance.Entities;
using Ordering.Persistance.Repositories.Psql;

public class RentalRequestRepository : IRentalRequestRepository
{
    private readonly NpgsqlConnection _connection;

    public RentalRequestRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> AddAsync(RentalRequest rentalRequest)
    {
        var query = @"
            INSERT INTO RentalRequests 
            (PropertyId, TenantId, UserId, StartDate, EndDate, NumberOfPeople, Pets, MessageToOwner, Status) 
            VALUES 
            (@PropertyId, @TenantId, @UserId, @StartDate, @EndDate, @NumberOfPeople, @Pets, @MessageToOwner, @Status) 
            RETURNING Id";

        var command = new NpgsqlCommand(query, _connection);
        command.Parameters.AddWithValue("@PropertyId", rentalRequest.PropertyId);
        command.Parameters.AddWithValue("@TenantId", rentalRequest.TenantId);
        command.Parameters.AddWithValue("@UserId", rentalRequest.UserId);
        command.Parameters.AddWithValue("@StartDate", rentalRequest.StartDate);
        command.Parameters.AddWithValue("@EndDate", rentalRequest.EndDate);
        command.Parameters.AddWithValue("@NumberOfPeople", rentalRequest.NumberOfPeople);
        command.Parameters.AddWithValue("@Pets", rentalRequest.Pets);
        command.Parameters.AddWithValue("@MessageToOwner", rentalRequest.MessageToOwner);
        command.Parameters.AddWithValue("@Status", rentalRequest.Status);

        await _connection.OpenAsync();
        var id = (int)await command.ExecuteScalarAsync();
        await _connection.CloseAsync();

        return id;
    }

    public async Task<RentalRequest> GetRentalRequestByOwnerIdAndOfferIdAsync(string ownerId, string offerId)
    {
        var query = @"
        SELECT rr.* FROM RentalRequests rr
        WHERE rr.UserId = @OwnerId AND rr.Id = @OfferId";

        var command = new NpgsqlCommand(query, _connection);
        command.Parameters.AddWithValue("@OwnerId", ownerId);
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
                StartDate = reader.GetDateTime(4),
                EndDate = reader.GetDateTime(5),
                NumberOfPeople = reader.GetInt32(6),
                Pets = reader.GetBoolean(7),
                MessageToOwner = reader.IsDBNull(8) ? null : reader.GetString(8),
                Status = reader.GetString(9)
            };
        }

        await _connection.CloseAsync();
        return rentalRequest;
    }


    public async Task<List<RentalRequest>> GetRentalRequestsByOwnerIdAsync(string ownerId)
    {
        var query = @"
        SELECT rr.* FROM RentalRequests rr
        WHERE rr.UserId = @OwnerId";
        var command = new NpgsqlCommand(query, _connection);
        command.Parameters.AddWithValue("@OwnerId", ownerId);

        await _connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();

        var rentalRequests = new List<RentalRequest>();
        while (await reader.ReadAsync())
        {
            var rentalRequest = new RentalRequest
            {
                Id = reader.GetInt32(0),
                PropertyId = reader.GetString(1),
                TenantId = reader.GetString(2),
                UserId = reader.GetString(3),
                StartDate = reader.GetDateTime(4),
                EndDate = reader.GetDateTime(5),
                NumberOfPeople = reader.GetInt32(6),
                Pets = reader.GetBoolean(7),
                MessageToOwner = reader.IsDBNull(8) ? null : reader.GetString(8),
                Status = reader.GetString(9)
            };
            rentalRequests.Add(rentalRequest);
        }

        await _connection.CloseAsync();
        return rentalRequests;
    }
}
