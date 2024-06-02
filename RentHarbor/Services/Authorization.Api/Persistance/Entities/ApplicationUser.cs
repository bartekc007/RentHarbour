using Microsoft.AspNetCore.Identity;

namespace Authorization.Persistance.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string? RefreshToken { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime? LockoutEnd { get; set; }
    }
}
