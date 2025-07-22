using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TravelbilityApp.Infrastructure.Data.Models;

using static TravelbilityApp.Infrastructure.Data.JsonSeeder;

namespace TravelbilityApp.Infrastructure.Data.Configurations
{
    internal class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            var roomTypes = GetData<RoomType>(nameof(RoomType));

            builder.HasData(roomTypes);
        }
    }
}
