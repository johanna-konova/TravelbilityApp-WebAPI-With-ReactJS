using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TravelbilityApp.Infrastructure.Data.Models;

using static TravelbilityApp.Infrastructure.Data.JsonSeeder;

namespace TravelbilityApp.Infrastructure.Data.Configurations
{
    internal class BedTypeConfiguration : IEntityTypeConfiguration<BedType>
    {
        public void Configure(EntityTypeBuilder<BedType> builder)
        {
            var bedTypes = GetData<BedType>(nameof(BedType));

            builder.HasData(bedTypes);
        }
    }
}
