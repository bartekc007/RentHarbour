namespace Ordering.Application.Domain.Order.Common
{
    public class RentalOffer
    {
        public int Id { get; set; }
        public string PropertyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPeople { get; set; }
        public bool Pets { get; set; }
        public bool Accepted { get; set; }
        public string MessageToOwner { get; set; }
        public string Status { get; set; }
        public string PropertyName { get; set; }
        public string PropertyStreet { get; set; }
        public string PropertyCity { get; set; }
        public string PropertyState { get; set; }
        public string PropertyPostalCode { get; set; }
        public string PropertyCountry { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }

    }
}
