using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelbilityApp.Infrastructure.Data.Models;

namespace TravelbilityApp.Infrastructure.Data
{
    public class TravelbilityAppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public TravelbilityAppDbContext(DbContextOptions<TravelbilityAppDbContext> options)
            : base(options)
        {
        }
    }
}
