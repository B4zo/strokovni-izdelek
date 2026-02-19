using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class AppDbSeed
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            if (await db.Users.AnyAsync())
                return;


            var users = new[]
            {
            new Users
            {
                Username = "admin",
                DisplayName = "System Admin",
                Role = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                IsActivated = true
            },
            new Users
            {
                Username = "manager",
                DisplayName = "Workshop Manager",
                Role = "manager",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("manager123"),
                IsActivated = true
            },
            new Users
            {
                Username = "employee",
                DisplayName = "Office Employee",
                Role = "office",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("employee123"),
                IsActivated = true
            }
        };
            db.Users.AddRange(users);

            await db.SaveChangesAsync();
        }
    }
}
