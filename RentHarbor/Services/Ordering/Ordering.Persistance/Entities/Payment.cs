namespace Ordering.Persistance.Entities
{
    public class Payment
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string PropertyId { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaidDate { get; set; }
    }

}
