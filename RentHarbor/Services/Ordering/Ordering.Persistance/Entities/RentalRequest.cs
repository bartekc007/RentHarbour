using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Persistance.Entities
{
    public class RentalRequest
    {
        public int Id { get; set; }
        public string PropertyId { get; set; }  // MongoDB Property ID
        public string TenantId { get; set; }  // Owner ID
        public string UserId { get; set; }  // User ID
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPeople { get; set; }
        public bool Pets { get; set; }
        public string MessageToOwner { get; set; }
        public string Status { get; set; }  // e.g., Pending, Approved, Rejected
    }
}
