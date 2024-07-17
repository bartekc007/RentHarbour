using Ordering.Persistance.Entities;

namespace Ordering.Persistance.Repositories.Psql
{
    public interface IPaymentRepository
    {
        public Task AddAsync(Payment payment);
        public Task ModifyAsync(Payment payment);
        public Task<IEnumerable<Payment>> GetByUserIdAndPropertyIdAsync(string userId, string propertyId);
    }
    
}
