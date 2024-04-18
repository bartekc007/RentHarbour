using Authorization.Api.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Api.Persistance.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.UserName).HasMaxLength(100).IsRequired();
            builder.HasIndex(x=>x.UserName).IsUnique();

            builder.Property(x => x.Email).HasMaxLength(24).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
