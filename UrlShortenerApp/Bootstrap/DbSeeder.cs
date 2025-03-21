using Microsoft.AspNetCore.Identity;
using UrlShortenerApp.Entities;

namespace UrlShortenerApp.Bootstrap;

public static class DbSeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Seed Roles if they do not exist
        var adminRole = await roleManager.FindByNameAsync("Admin");
        if (adminRole == null)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        var userRole = await roleManager.FindByNameAsync("User");
        if (userRole == null)
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        // Seed Admin User if it doesn't exist
        var adminUser = await userManager.FindByEmailAsync("admin@example.com");
        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "Admin1234!");

            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
