
using GameVault.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace GameVault.Infrastructure
{
    public static class IdentitySeeder
    {
        public static async Task SeedAdminAsync(
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            UserManager<ApplicationUser> userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            const string adminRole = "Admin";

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                IdentityResult roleResult =
                    await roleManager.CreateAsync(new IdentityRole(adminRole));

                if (!roleResult.Succeeded)
                {
                    throw new Exception("Failed to create Admin role.");
                }
            }

            string? adminEmail = configuration["AdminAccount:Email"];
            string? adminPassword = configuration["AdminAccount:Password"];

            if (string.IsNullOrWhiteSpace(adminEmail) ||
                string.IsNullOrWhiteSpace(adminPassword))
            {
                throw new Exception(
                    "Development Admin credentials are not configured.");
            }

            ApplicationUser? admin =
                await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                IdentityResult createResult =
                    await userManager.CreateAsync(admin, adminPassword);

                if (!createResult.Succeeded)
                {
                    string errors = string.Join(
                        ", ",
                        createResult.Errors.Select(e => e.Description));

                    throw new Exception(
                        $"Failed to create Admin account: {errors}");
                }
            }

            if (!await userManager.IsInRoleAsync(admin, adminRole))
            {
                IdentityResult addRoleResult =
                    await userManager.AddToRoleAsync(admin, adminRole);

                if (!addRoleResult.Succeeded)
                {
                    throw new Exception(
                        "Failed to assign Admin role.");
                }
            }
        }
    }
}
