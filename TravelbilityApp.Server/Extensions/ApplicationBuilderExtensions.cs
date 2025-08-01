using Microsoft.AspNetCore.Identity;

using TravelbilityApp.Infrastructure.Data.Models;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public const string AdminRoleName = "Administrator";
        public const string AdminUserId = "bcb4f072-ecca-43c9-ab26-c060c6f364e4";
        public const string AdminUserEmail = "admin@mail.com";
        public const string AdminUserPassword = "Admin_123";

        public static async Task<IApplicationBuilder> SeedAdminRoleAsync(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            if (await roleManager.RoleExistsAsync(AdminRoleName) == false)
            {
                var role = new ApplicationRole()
                {
                    Name = AdminRoleName,
                    NormalizedName = AdminRoleName.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                };

                await roleManager.CreateAsync(role);
            }

            return app;
        }

        public static async Task<IApplicationBuilder> AssignAdminRoleAsync(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var adminUser = await userManager.FindByEmailAsync(AdminUserEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = AdminUserEmail,
                    Email = AdminUserEmail,
                };

                var createAdminUserResult = await userManager.CreateAsync(adminUser, AdminUserPassword);

                if (createAdminUserResult.Succeeded == false)
                {
                    throw new Exception($"Failed to create admin user: {AdminUserEmail}");
                }
            }

            var isUserInRole = await userManager.IsInRoleAsync(adminUser, AdminRoleName);

            if (isUserInRole == false)
            {
                var addUserToAdminRoleResult = await userManager.AddToRoleAsync(adminUser, AdminRoleName);

                if (addUserToAdminRoleResult.Succeeded == false)
                {
                    throw new Exception($"Failed to assign admin role to user: {AdminUserEmail}");
                }
            }

            return app;
        }
    }
}
