using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.Globalization;

using TravelbilityApp.Infrastructure.Data.Models;
using TravelbilityApp.Infrastructure.Data.Models.Enums;

using static TravelbilityApp.Infrastructure.Data.Constants.SeedDataConstants;

namespace TravelbilityApp.Infrastructure.Data.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasData(GenerateProperties());
        }

        private IEnumerable<Property> GenerateProperties()
        {
            var properties = new HashSet<Property>();

            var property = new Property()
            {
                Id = Guid.Parse(FirstPropertyId),
                Name = "FIVE Palm Jumeirah Dubai",
                PropertyTypeId = 3,
                StarsCount = 5,
                CheckIn = TimeOnly.ParseExact("15:00", "HH:ss", CultureInfo.InvariantCulture, DateTimeStyles.None),
                CheckOut = TimeOnly.ParseExact("06:00", "HH:ss", CultureInfo.InvariantCulture, DateTimeStyles.None),
                Address = "Palm Jumeirah, Palm Jumeirah, Dubai, United Arab Emirates",
                Description = "FIVE Palm Jumeirah Dubai features its own private beach as well as 5 outdoor swimming pools, including a 180 ft long option, running through the heart of the resort. Guests can enjoy free WiFi throughout the property.\r\n\r\nThe hotel has 470 guest rooms and suites, spread across 16 floors, decorated in a simple yet elegant style with views of the Arabian Gulf.\r\n\r\nThe resort has an array of facilities, including dining venues hosted by world-class chefs, a modern spa and a karaoke room at Maiden Shanghai.\r\n\r\nA landmark on the trunk of the iconic Palm Jumeirah, FIVE Palm Jumeirah Dubai is strategically located for convenient access to Dubai’s business districts, as well as the city’s many exciting tourist and entertainment attractions.\r\n\r\nThe resort is also accessible from Dubai International Airport (DXB), which is 20 mi away and Al Maktoum International Airport (DWC), which 26 mi away. Mall of Emirates is 7 mi away, while Dubai Mall is 14 mi from the property.",
                PublisherId = Guid.Parse(PeterId),
                Status = PropertyStatus.Published,
            };
            properties.Add(property);

            property = new Property()
            {
                Id = Guid.Parse(SecondPropertyId),
                Name = "Hilton Garden Inn Istanbul Beylikduzu",
                PropertyTypeId = 1,
                StarsCount = 4,
                CheckIn = TimeOnly.ParseExact("14:00", "HH:ss", CultureInfo.InvariantCulture, DateTimeStyles.None),
                CheckOut = TimeOnly.ParseExact("00:00", "HH:ss", CultureInfo.InvariantCulture, DateTimeStyles.None),
                Address = "Barbaros Hayrettin Pasa Mah. 1999 Sok. Esenyurt, 34522 Istanbul, Turkey",
                Description = "Located in business area in Beylikduzu, 1 km from Tuyap Convention Centre, Hilton Garden Inn Istanbul Beylikduzu features indoor pool and 24/7 fitness centre. Free WiFi access is available in all areas.\n\nModern rooms are fitted with a flat-screen TV. Some units include a seating area for your convenience. Every room comes with a private bathroom. For your comfort, you will find free toiletries and a hair dryer.\n\nThere is a 24-hour front desk, providing room service at the property. Laundry, dry cleaning and ironing services are also provided upon request at an additional charge.\n\nGuests can enjoy their meals at the on-site restaurant. The lobby bar is ideal for having a drink and relaxing after a busy day.\n\nThe hotel is 35 km from Istanbul’s historic centre, where guests can visit Topkapi Palace, Blue Mosque, and Hagia Sophia Museum. Istanbul Airport is a 50-minute drive away.",
                PublisherId = Guid.Parse(PeterId),
            };
            properties.Add(property);

            property = new Property()
            {
                Id = Guid.Parse(ThirdPropertyId),
                Name = "Summer Beach Maldives",
                PropertyTypeId = 5,
                StarsCount = 4,
                CheckIn = TimeOnly.ParseExact("14:00", "HH:ss", CultureInfo.InvariantCulture, DateTimeStyles.None),
                CheckOut = TimeOnly.ParseExact("12:00", "HH:ss", CultureInfo.InvariantCulture, DateTimeStyles.None),
                Address = "Boduthakurufaanu Magu, 20006 Male City, Maldives",
                Description = "Beachfront Location: Summer Beach Maldives in Male City offers direct beachfront access with stunning sea views. Guests enjoy a terrace and outdoor seating area, perfect for relaxation.\r\n\r\nComfortable Accommodations: Rooms feature air-conditioning, private bathrooms, and modern amenities such as free WiFi, mini-bars, and flat-screen TVs. Family rooms and interconnected rooms cater to all travelers.\r\n\r\nDining Experience: The family-friendly restaurant serves Indian, Italian, Thai, and international cuisines, including vegetarian and halal options. Breakfast includes local specialties, fresh pastries, and a variety of beverages.\r\n\r\nConvenient Services: The guest house provides a free airport shuttle service, 24-hour front desk, concierge, and tour desk. Additional amenities include a coffee shop, child-friendly buffet, and free WiFi throughout the property.",
                PublisherId = Guid.Parse(GeorgeId),
                Status = PropertyStatus.Published,
            };
            properties.Add(property);

            return properties;
        }
    }
}
