using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TravelbilityApp.Infrastructure.Data.Models;

using static TravelbilityApp.Infrastructure.Data.JsonSeeder;

namespace TravelbilityApp.Infrastructure.Data.Configurations
{
    internal class PropertyTypeConfiguration : IEntityTypeConfiguration<PropertyType>
    {
        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            var propertyTypes = GetData<PropertyType>(nameof(PropertyType));

            builder.HasData(propertyTypes);
        }
    }
}
