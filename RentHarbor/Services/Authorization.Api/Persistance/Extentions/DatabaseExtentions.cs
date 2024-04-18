using Authorization.Api.Persistance.Context;
using Authorization.Api.Persistance.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Api.Persistance.Extensions
{
    public static class DatabaseExtentions
    {
        public static async Task InitialiseDatabaseAsync(this IApplicationBuilder app, AuthDbContext dbContext)
        {
            await dbContext.Database.MigrateAsync();
        }

        public static async Task SeedDatabaseAsync(this IApplicationBuilder app, AuthDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            if (!dbContext.Users.Any())
            {
                // Dodaj przykładowych użytkowników
                await AddSampleUsersAsync(userManager);
            }
        }

        private static async Task AddSampleUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var user1 = new ApplicationUser { UserName = "user1@example.com", Email = "user1@example.com" };
            var user2 = new ApplicationUser { UserName = "user2@example.com", Email = "user2@example.com" };

            await userManager.CreateAsync(user1, "Password1234!");
            await userManager.CreateAsync(user2, "Password1234!");
        }
    }
}
