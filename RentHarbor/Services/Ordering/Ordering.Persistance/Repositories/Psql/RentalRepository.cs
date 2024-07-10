using Dapper;
using Npgsql;
using Ordering.Persistance.Entities;
using Ordering.Persistance.Repositories.Psql;

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
        (Id, PropertyId, RentalRequestId, RenterId) 
        VALUES 
        (@Id, @PropertyId, @RentalRequestId, @RenterId)";

        using (var connection = _connection)
        {
            connection.Open();
            var result = await connection.ExecuteAsync(query, rental);
            return result;
        }
    }

    public async Task<bool> ModifyAsync(Rental rental)
    {
        var query = @"
        UPDATE Rentals 
        SET PropertyId = @PropertyId, 
            RentalRequestId = @RentalRequestId, 
            RenterId = @RenterId
        WHERE Id = @Id";

        using (var connection = _connection)
        {
            connection.Open();
            var rowsAffected = await connection.ExecuteAsync(query, rental);
            return rowsAffected > 0;
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var query = "DELETE FROM Rentals WHERE Id = @Id";

        using (var connection = _connection)
        {
            connection.Open();
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }

    public async Task<Rental> GetByIdAsync(string id)
    {
        var query = "SELECT * FROM Rentals WHERE Id = @Id";

        using (var connection = _connection)
        {
            connection.Open();
            var rental = await connection.QuerySingleOrDefaultAsync<Rental>(query, new { Id = id });
            return rental;
        }
    }

    public async Task<IEnumerable<Rental>> GetByUserIdAsync(string userId)
    {
        var query = "SELECT * FROM Rentals WHERE RenterId = @UserId";

        using (var connection = _connection)
        {
            connection.Open();
            var rentals = await connection.QueryAsync<Rental>(query, new { UserId = userId });
            return rentals;
        }
    }
}
