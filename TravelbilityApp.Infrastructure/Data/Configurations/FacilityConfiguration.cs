using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TravelbilityApp.Infrastructure.Data.Models;

using static TravelbillityApp.Infrastructure.Data.JsonSeeder;

namespace TravelbillityApp.Infrastructure.Data.Configurations
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
