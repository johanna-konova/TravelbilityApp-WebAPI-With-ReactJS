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

        public DbSet<BedType> BedTypes { get; init; }
        public DbSet<RoomType> RoomTypes { get; init; }
        public DbSet<Room> Rooms { get; init; }
        public DbSet<PropertyType> PropertyTypes { get; init; }
        public DbSet<Facility> Facilities { get; init; }
        public DbSet<Property> Properties { get; init; }
        public DbSet<PropertyFacility> PropertiesFacilities { get; init; }
        public DbSet<PropertyPhoto> PropertiesPhotos { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
