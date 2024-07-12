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
        (PropertyId, TenantId, UserId, OwnerAcceptance, UserAcceptance, StartDate, EndDate, NumberOfPeople, Pets, MessageToOwner, Status) 
        VALUES 
        (@PropertyId, @TenantId, @UserId, @OwnerAcceptance, @UserAcceptance, @StartDate, @EndDate, @NumberOfPeople, @Pets, @MessageToOwner, @Status) 
        RETURNING Id";

        var command = new NpgsqlCommand(query, _connection);
        command.Parameters.AddWithValue("@PropertyId", rentalRequest.PropertyId);
        command.Parameters.AddWithValue("@TenantId", rentalRequest.TenantId);
        command.Parameters.AddWithValue("@UserId", rentalRequest.UserId);
        command.Parameters.AddWithValue("@OwnerAcceptance", rentalRequest.OwnerAcceptance);
        command.Parameters.AddWithValue("@UserAcceptance", rentalRequest.UserAcceptance);
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

    public async Task<bool> ModifyAsync(RentalRequest rentalRequest)
    {
        var query = @"
        UPDATE RentalRequests 
        SET PropertyId = @PropertyId, 
            TenantId = @TenantId, 
            UserId = @UserId, 
            OwnerAcceptance = @OwnerAcceptance, 
            UserAcceptance = @UserAcceptance, 
            StartDate = @StartDate, 
            EndDate = @EndDate, 
            NumberOfPeople = @NumberOfPeople, 
            Pets = @Pets, 
            MessageToOwner = @MessageToOwner, 
            Status = @Status
        WHERE Id = @Id";

        var command = new NpgsqlCommand(query, _connection);
        command.Parameters.AddWithValue("@Id", rentalRequest.Id);
        command.Parameters.AddWithValue("@PropertyId", rentalRequest.PropertyId);
        command.Parameters.AddWithValue("@TenantId", rentalRequest.TenantId);
        command.Parameters.AddWithValue("@UserId", rentalRequest.UserId);
        command.Parameters.AddWithValue("@OwnerAcceptance", rentalRequest.OwnerAcceptance);
        command.Parameters.AddWithValue("@UserAcceptance", rentalRequest.UserAcceptance);
        command.Parameters.AddWithValue("@StartDate", rentalRequest.StartDate);
        command.Parameters.AddWithValue("@EndDate", rentalRequest.EndDate);
        command.Parameters.AddWithValue("@NumberOfPeople", rentalRequest.NumberOfPeople);
        command.Parameters.AddWithValue("@Pets", rentalRequest.Pets);
        command.Parameters.AddWithValue("@MessageToOwner", rentalRequest.MessageToOwner);
        command.Parameters.AddWithValue("@Status", rentalRequest.Status);

        await _connection.OpenAsync();
        var rowsAffected = await command.ExecuteNonQueryAsync();
        await _connection.CloseAsync();

        return rowsAffected > 0;
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

    public async Task<RentalRequest> GetRentalRequestByOwnerIdAndOfferIdAsync(string ownerId, int offerId)
    {
        try
        {
            var query = @"
    SELECT rr.* FROM RentalRequests rr
    WHERE rr.TenantId = @OwnerId AND rr.Id = @OfferId";

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



    public async Task<List<RentalRequest>> GetRentalRequestsByOwnerIdAsync(string ownerId)
    {
        try
        {
            var query = @"
    SELECT rr.* FROM RentalRequests rr
    WHERE rr.TenantId = @OwnerId";
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
                    OwnerAcceptance = reader.GetBoolean(4),
                    UserAcceptance = reader.GetBoolean(5),
                    StartDate = reader.GetDateTime(6),
                    EndDate = reader.GetDateTime(7),
                    NumberOfPeople = reader.GetInt32(8),
                    Pets = reader.GetBoolean(9),
                    MessageToOwner = reader.IsDBNull(10) ? null : reader.GetString(10),
                    Status = reader.GetString(11)
                };
                rentalRequests.Add(rentalRequest);
            }

            await _connection.CloseAsync();
            return rentalRequests;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<List<RentalRequest>> GetRentalRequestsByUserIdAsync(string userId)
    {
        try
        {
            var query = @"
    SELECT rr.* FROM RentalRequests rr
    WHERE rr.UserId = @UserId";
            var command = new NpgsqlCommand(query, _connection);
            command.Parameters.AddWithValue("@UserId", userId);

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
                    OwnerAcceptance = reader.GetBoolean(4),
                    UserAcceptance = reader.GetBoolean(5),
                    StartDate = reader.GetDateTime(6),
                    EndDate = reader.GetDateTime(7),
                    NumberOfPeople = reader.GetInt32(8),
                    Pets = reader.GetBoolean(9),
                    MessageToOwner = reader.IsDBNull(10) ? null : reader.GetString(10),
                    Status = reader.GetString(11)
                };
                rentalRequests.Add(rentalRequest);
            }

            await _connection.CloseAsync();
            return rentalRequests;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var query = @"
        DELETE FROM RentalRequests 
        WHERE Id = @Id";

        var command = new NpgsqlCommand(query, _connection);
        command.Parameters.AddWithValue("@Id", id);

        await _connection.OpenAsync();
        var rowsAffected = await command.ExecuteNonQueryAsync();
        await _connection.CloseAsync();

        return rowsAffected > 0;
    }
}
