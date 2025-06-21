using Microsoft.EntityFrameworkCore;
using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Property;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;

namespace TravelbilityApp.Core.Services
{
    public class PropertyService(
        IRepository repository,
        IFacilityService facilityService) : IPropertyService
    {
        public async Task<PropertyDetailsDto?> GetByIdAsync(Guid id)
            => await repository
                .AllAsNoTracking<Property>()
                .Where(p => p.Id == id)
                .Select(p => new PropertyDetailsDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StarsCount = p.StarsCount,
                    CheckIn = p.CheckIn,
                    CheckOut = p.CheckOut,
                    Address = p.Address,
                    Description = p.Description,
                    PublisherId = p.PublisherId,
                    Type = new PropertyTypeOptionDto()
                    {
                        Id = p.PropertyType.Id,
                        Name = p.PropertyType.Name,
                    },
                    Facilities = p.Facilities
                        .Select(f => new PropertyFacilityOptionDto()
                        {
                            Id = f.FacilityId,
                            Name = f.Facility.Name,
                            IsForAccessibility = f.Facility.IsForAccessibility,
                        }),
                    ImageUrls = p.Photos
                        .Select(p => p.Url)
                })
                .SingleOrDefaultAsync();

        public async Task<Guid> CreateAsync(CreatePropertyDto dto, Guid userId)
        {
            var newProperty = new Property()
            {
                Name = dto.Name,
                StarsCount = dto.StarsCount,
                CheckIn = (TimeOnly)dto.CheckIn!,
                CheckOut = (TimeOnly)dto.CheckOut!,
                Address = dto.Address,
                Description = dto.Description,
                PropertyTypeId = (int)dto.TypeId!,
                PublisherId = userId,
            };

            var selectedFacilityIds = dto.FacilityIds
                .Distinct()
                .Where(fi => fi != null);

            var validSelectedFacilityIds = await facilityService.GetValidSelectedIdsAsync(selectedFacilityIds);

            var newPropertyFacilities = validSelectedFacilityIds
                .Select(vsfi => new PropertyFacility()
                {
                    PropertyId = newProperty.Id,
                    FacilityId = vsfi,
                });

            var validImageUrls = dto.ImageUrls
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Where(iu => Uri.TryCreate(iu, UriKind.Absolute, out var createdUrl) &&
                             (createdUrl.Scheme == Uri.UriSchemeHttp || createdUrl.Scheme == Uri.UriSchemeHttps));

            var newPropertyPhotos = validImageUrls
                .Select(viu => new PropertyPhoto()
                {
                    Url = viu,
                    PropertyId = newProperty.Id,
                });

            await repository.AddAsync(newProperty);
            await repository.AddRangeAsync(newPropertyFacilities);
            await repository.AddRangeAsync(newPropertyPhotos);

            await repository.SaveChangesAsync();

            return newProperty.Id;
        }
    }
}
