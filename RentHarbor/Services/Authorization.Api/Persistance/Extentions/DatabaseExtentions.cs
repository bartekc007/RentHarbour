using Authorization.Persistance.Context;
using Authorization.Persistance.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Persistance.Extentions
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
            var user1 = new ApplicationUser
            {
                UserName = "user1@example.com",
                Email = "user1@example.com",
                Address = "Krakowska 1",
                City = "Kraków",
                Country = "Polska"
            };
            var user2 = new ApplicationUser
            {
                UserName = "user2@example.com",
                Email = "user2@example.com",
                Address = "Krakowska 2",
                City = "Kraków",
                Country = "Polska"
            };

            await userManager.CreateAsync(user1, "Password1234!");
            await userManager.CreateAsync(user2, "Password1234!");
        }
    }
}
