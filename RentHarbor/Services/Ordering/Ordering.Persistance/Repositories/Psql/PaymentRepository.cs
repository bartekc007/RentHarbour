using Dapper;
using Npgsql;
using Ordering.Persistance.Entities;

namespace Ordering.Persistance.Repositories.Psql
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly NpgsqlConnection _connection;

        public PaymentRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task AddAsync(Payment payment)
        {
            var query = @"
        INSERT INTO Payments (Id, UserId, PropertyId, PaymentDate, IsPaid, Amount, PaidDate)
        VALUES (@Id, @UserId, @PropertyId, @PaymentDate, @IsPaid, @Amount, @PaidDate)";

            await _connection.OpenAsync();
            await _connection.ExecuteAsync(query, payment);
            await _connection.CloseAsync();
        }

        public async Task<IEnumerable<Payment>> GetByUserIdAndPropertyIdAsync(string userId, string propertyId)
        {
            var query = "SELECT * FROM Payments WHERE UserId = @UserId AND PropertyId = @PropertyId";

            await _connection.OpenAsync();
            var resullt = await _connection.QueryAsync<Payment>(query, new { UserId = userId, PropertyId = propertyId });
            await _connection.CloseAsync();
            return resullt;
        }

        public async Task ModifyAsync(Payment payment)
        {
            var query = @"
        UPDATE Payments 
        SET UserId = @UserId,
            PropertyId = @PropertyId,
            PaymentDate = @PaymentDate,
            IsPaid = @IsPaid,
            Amount = @Amount,
            PaidDate = @PaidDate
        WHERE Id = @Id";

            await _connection.OpenAsync();
            await _connection.ExecuteAsync(query, payment);
            await _connection.CloseAsync();
        }
    }

}
