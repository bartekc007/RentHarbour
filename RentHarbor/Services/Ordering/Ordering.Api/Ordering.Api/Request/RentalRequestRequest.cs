using System;

namespace Ordering.Api.Request
{
    public class RentalRequestRequest
    {
        public string PropertyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPeople { get; set; }
        public bool Pets { get; set; }
        public string MessageToOwner { get; set; }
    }
}
