using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TravelbilityApp.Infrastructure.Data.Models;

using static TravelbilityApp.Infrastructure.Data.Constants.SeedDataConstants;

namespace TravelbilityApp.Infrastructure.Data.Configurations
{
    public class PropertyFacilityConfiguration : IEntityTypeConfiguration<PropertyFacility>
    {
        public void Configure(EntityTypeBuilder<PropertyFacility> builder)
        {
            builder.HasData(GeneratePropertiesFacilities());
        }

        private IEnumerable<PropertyFacility> GeneratePropertiesFacilities()
        {
            var propertiesFacilities = new HashSet<PropertyFacility>();

            int[] firstPropertyFacilityIds = [1, 2, 3, 4, 5, 7, 8, 9, 10, 12, 13, 14, 15, 16, 17, 18, 19, 20, 34, 39, 42];

            foreach (var facilityId in firstPropertyFacilityIds)
            {
                var propertyFacility = new PropertyFacility()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(FirstPropertyId),
                    FacilityId = facilityId,
                };

                propertiesFacilities.Add(propertyFacility);
            }

            int[] firstPropertyFirstRoomFacilityIds = [1, 3, 4, 5, 6, 7, 8, 11, 12, 13, 15, 16, 19, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 35, 36, 37, 38, 40, 41, 43];

            foreach (var facilityId in firstPropertyFirstRoomFacilityIds)
            {
                var propertyFacility = new PropertyFacility()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(FirstPropertyId),
                    RoomId = Guid.Parse(FirstPropertyFirstRoomId),
                    FacilityId = facilityId,
                };

                propertiesFacilities.Add(propertyFacility);
            }

            int[] firstPropertySecondRoomFacilityIds = [3, 4, 11, 22, 26, 31, 38];

            foreach (var facilityId in firstPropertySecondRoomFacilityIds)
            {
                var propertyFacility = new PropertyFacility()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(FirstPropertyId),
                    RoomId = Guid.Parse(FirstPropertySecondRoomId),
                    FacilityId = facilityId,
                };

                propertiesFacilities.Add(propertyFacility);
            }

            int[] secondPropertyFacilityIds = [1, 2, 3, 9, 10, 16, 17, 39, 42];

            foreach (var facilityId in secondPropertyFacilityIds)
            {
                var propertyFacility = new PropertyFacility()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(SecondPropertyId),
                    FacilityId = facilityId,
                };

                propertiesFacilities.Add(propertyFacility);
            }

            int[] secondPropertyFirstRoomFacilityIds = [3, 21, 24, 26, 27, 28, 30, 33, 38];

            foreach (var facilityId in secondPropertyFirstRoomFacilityIds)
            {
                var propertyFacility = new PropertyFacility()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(SecondPropertyId),
                    RoomId = Guid.Parse(SecondPropertyFirstRoomId),
                    FacilityId = facilityId,
                };

                propertiesFacilities.Add(propertyFacility);
            }

            int[] thirdPropertyFacilityIds = [1, 5, 8, 13, 9, 17, 20, 34, 42];

            foreach (var facilityId in thirdPropertyFacilityIds)
            {
                var propertyFacility = new PropertyFacility()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(ThirdPropertyId),
                    FacilityId = facilityId,
                };

                propertiesFacilities.Add(propertyFacility);
            }

            int[] thirdPropertyFirstRoomFacilityIds = [4, 7, 12, 13, 19, 23, 28, 32, 36, 38, 43];

            foreach (var facilityId in thirdPropertyFirstRoomFacilityIds)
            {
                var propertyFacility = new PropertyFacility()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = Guid.Parse(ThirdPropertyId),
                    RoomId = Guid.Parse(ThirdPropertyFirstRoomId),
                    FacilityId = facilityId,
                };

                propertiesFacilities.Add(propertyFacility);
            }

            return propertiesFacilities;
        }
    }
}
