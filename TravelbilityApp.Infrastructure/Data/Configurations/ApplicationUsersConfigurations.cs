using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TravelbilityApp.Infrastructure.Data.Models;

using static TravelbilityApp.Infrastructure.Data.Constants.SeedDataConstants;

namespace TravelbilityApp.Infrastructure.Data.Configurations
{
    internal class ApplicationUsersConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(GenerateUsers());
        }

        private IEnumerable<ApplicationUser> GenerateUsers()
        {
            var users = new HashSet<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            ApplicationUser user = new ApplicationUser()
            {
                Id = Guid.Parse(PeterId),
                UserName = PeterEmail,
                NormalizedUserName = PeterEmail.ToUpper(),
                Email = PeterEmail,
                NormalizedEmail = PeterEmail.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = "XKYK5BIDWLG3ED57QZYQHRLUZMMUVYWS",
                ConcurrencyStamp = "7f3fbb67-83a1-4c8a-9376-77d5bc93e96e",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
            
            user.PasswordHash = hasher.HashPassword(user, PeterPassword);
            users.Add(user);

            user = new ApplicationUser()
            {
                Id = Guid.Parse(GeorgeId),
                UserName = GeorgeEmail,
                NormalizedUserName = GeorgeEmail.ToUpper(),
                Email = GeorgeEmail,
                NormalizedEmail = GeorgeEmail.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = "JTHY3GADWFA4KD67TFYUIUQNLJMNXYAS",
                ConcurrencyStamp = "5d4fdb87-23b1-4da2-9e3e-a8b4df7a283f",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            user.PasswordHash = hasher.HashPassword(user, GeorgePassword);
            users.Add(user);

            return users;
        }
    }
}
