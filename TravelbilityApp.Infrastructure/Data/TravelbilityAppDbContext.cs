using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System.Reflection;

using TravelbilityApp.Infrastructure.Data.Models;

namespace TravelbilityApp.Infrastructure.Data
{
    public class TravelbilityAppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public TravelbilityAppDbContext(DbContextOptions<TravelbilityAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<PropertyType> PropertyTypes { get; init; }
        public DbSet<Facility> Facilities { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
