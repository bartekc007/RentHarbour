namespace Ordering.Persistance.Entities
{
    public class Payment
    {
        public string Id { get; set; } // Unique identifier for the payment
        public string UserId { get; set; }
        public string PropertyId { get; set; }
        public DateTime PaymentDate { get; set; } // Date when the payment is due
        public bool IsPaid { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaidDate { get; set; } // Date when the payment was made
    }

}
