namespace Catalog.Persistance.Entities
{
    public class Rental
    {
        public string Id { get; set; }
        public string PropertyId { get; set; }
        public string RentalRequestId { get; set; }
        public string RenterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
