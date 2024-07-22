using Catalog.Persistance.Entities;
using Catalog.Persistance.Repositories.Psql;
using Dapper;
using System.Data;

public class RentalRepository : IRentalRepository
{
    private readonly IDbConnection _connection;

    public RentalRepository(IDbConnection connection)
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

        return await _connection.ExecuteAsync(query, rental);
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

        var rowsAffected = await _connection.ExecuteAsync(query, rental);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(string rentalId)
    {
        var query = "DELETE FROM Rentals WHERE Id = @Id";

        var rowsAffected = await _connection.ExecuteAsync(query, new { Id = rentalId });
        return rowsAffected > 0;
    }

    public async Task<Rental> GetByIdAsync(string rentalId)
    {
        var query = "SELECT * FROM Rentals WHERE Id = @Id";

        return await _connection.QuerySingleOrDefaultAsync<Rental>(query, new { Id = rentalId });
    }

    public async Task<IEnumerable<Rental>> GetRentalsByUserIdAsync(string userId)
    {
        var query = "SELECT * FROM Rentals WHERE RenterId = @UserId";

        return await _connection.QueryAsync<Rental>(query, new { UserId = userId });
    }
}
