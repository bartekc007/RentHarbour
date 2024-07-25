using Authorization.Persistance.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Authorization.Persistance.Context
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext()
        {
            
        }
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> Users => Set<ApplicationUser>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.AccessFailedCount).HasDefaultValue(0);
                entity.Property(e => e.LockoutEnd).HasColumnType("datetime");
            });
        }
    }
}
