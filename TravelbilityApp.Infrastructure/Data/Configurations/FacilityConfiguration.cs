using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TravelbilityApp.Infrastructure.Data.Models;

using static TravelbilityApp.Infrastructure.Data.JsonSeeder;

namespace TravelbilityApp.Infrastructure.Data.Configurations
{
    internal class FacilityConfiguration : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            var facilities = GetData<Facility>(nameof(Facility));

            builder.HasData(facilities);
        }
    }
}
