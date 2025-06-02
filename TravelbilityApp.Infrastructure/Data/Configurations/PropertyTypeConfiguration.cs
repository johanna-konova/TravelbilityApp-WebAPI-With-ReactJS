using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TravelbilityApp.Infrastructure.Data.Models;

using static TravelbillityApp.Infrastructure.Data.JsonSeeder;

namespace TravelbillityApp.Infrastructure.Data.Configurations
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
