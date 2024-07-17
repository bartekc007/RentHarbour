using Catalog.Persistance.Entities;
using Catalog.Persistance.Repositories.Psql;
using Dapper;
using Npgsql;

public class RentalRepository : IRentalRepository
{
    private readonly NpgsqlConnection _connection;

    public RentalRepository(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> AddAsync(Rental rental)
    {
        var query = @"
        INSERT INTO Rentals 
        (Id, PropertyId, RentalRequestId, RenterId, StartDate, EndDate) 
        VALUES 
        (@Id, @PropertyId, @RentalRequestId, @RenterId, @StartDate, @EndDate)";

        await _connection.OpenAsync();
        var result = await _connection.ExecuteAsync(query, rental);
        await _connection.CloseAsync();
        return result;
    }

    public async Task<bool> ModifyAsync(Rental rental)
    {
        var query = @"
        UPDATE Rentals 
        SET PropertyId = @PropertyId, 
            RentalRequestId = @RentalRequestId, 
            RenterId = @RenterId,
            StartDate = @StartDate,
            EndDate = @EndDate
        WHERE Id = @Id";

        await _connection.OpenAsync();
        var rowsAffected = await _connection.ExecuteAsync(query, rental);
        await _connection.CloseAsync();
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var query = "DELETE FROM Rentals WHERE Id = @Id";

        await _connection.OpenAsync();
        var rowsAffected = await _connection.ExecuteAsync(query, new { Id = id });
        await _connection.CloseAsync();
        return rowsAffected > 0;
    }

    public async Task<Rental> GetByIdAsync(string id)
    {
        var query = "SELECT * FROM Rentals WHERE Id = @Id";

        await _connection.OpenAsync();
        var rental = await _connection.QuerySingleOrDefaultAsync<Rental>(query, new { Id = id });
        await _connection.CloseAsync();
        return rental;
    }

    public async Task<IEnumerable<Rental>> GetByUserIdAsync(string userId)
    {
        var query = "SELECT * FROM Rentals WHERE RenterId = @UserId";

        await _connection.OpenAsync();
        var rentals = await _connection.QueryAsync<Rental>(query, new { UserId = userId });
        await _connection.CloseAsync();
        return rentals;
    }
}
