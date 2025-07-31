using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelbilityApp.Infrastructure.Data.Models;

using static TravelbilityApp.Infrastructure.Data.Constants.SeedDataConstants;

namespace TravelbilityApp.Infrastructure.Data.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasData(GenerateRooms());
        }

        private IEnumerable<Room> GenerateRooms()
        {
            var rooms = new HashSet<Room>();

            var room = new Room()
            {
                Id = Guid.Parse(FirstPropertyFirstRoomId),
                RoomTypeId = 8,
                MaxGuests = 4,
                PricePerNight = 129.99M,
                MainBedTypeId = 3,
                SizeInSquareMeters = 25,
                NumberOfUnits = 3,
                Description = "Featuring a private balcony and seating, this air conditioned room offers a large double bed, a 55 inch smart TV.",
                PropertyId = Guid.Parse(FirstPropertyId),
            };
            rooms.Add(room);

            room = new Room()
            {
                Id = Guid.Parse(FirstPropertySecondRoomId),
                RoomTypeId = 6,
                MaxGuests = 2,
                PricePerNight = 357,
                MainBedTypeId = 5,
                SizeInSquareMeters = 55,
                NumberOfUnits = 11,
                Description = "This spacious suite is consisted of of 1 bedroom, a seating area and 1 bathroom with a walk-in shower and a bath. The air-conditioned suite offers a flat-screen TV with streaming services, soundproof walls, a mini-bar, a tea and coffee maker as well as city views. The unit offers 1 bed.",
                PropertyId = Guid.Parse(FirstPropertyId),
            };
            rooms.Add(room);

            room = new Room()
            {
                Id = Guid.Parse(SecondPropertyFirstRoomId),
                RoomTypeId = 3,
                MaxGuests = 2,
                PricePerNight = 109.98M,
                MainBedTypeId = 2,
                SizeInSquareMeters = 39,
                NumberOfUnits = 7,
                Description = "The double room offers air conditioning, a tea and coffee maker, a safe deposit box, heating and a flat-screen TV. The unit offers 2 beds.",
                PropertyId = Guid.Parse(SecondPropertyId),
            };
            rooms.Add(room);

            room = new Room()
            {
                Id = Guid.Parse(ThirdPropertyFirstRoomId),
                RoomTypeId = 8,
                MaxGuests = 3,
                PricePerNight = 197,
                MainBedTypeId = 3,
                SizeInSquareMeters = 43,
                NumberOfUnits = 5,
                Description = "The air-conditioned suite features 2 bedrooms and 2 bathrooms with a bath and a shower. Featuring a balcony with sea views, this suite also provides a mini-bar and a flat-screen TV with cable channels. The unit offers 2 beds.",
                PropertyId = Guid.Parse(ThirdPropertyId),
            };
            rooms.Add(room);

            return rooms;
        }
    }
}
