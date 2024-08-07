﻿namespace Communication.Persistance.Entities
{
    public class RentalRequest
    {
        public int Id { get; set; }
        public string PropertyId { get; set; }
        public string TenantId { get; set; }
        public string UserId { get; set; }
        public bool OwnerAcceptance { get; set; }
        public bool UserAcceptance { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPeople { get; set; }
        public bool Pets { get; set; }
        public string MessageToOwner { get; set; }
        public string Status { get; set; }
    }
}
