using Microsoft.EntityFrameworkCore;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Property;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;

using static TravelbilityApp.Core.CommonHelpers;

namespace TravelbilityApp.Core.Services
{
    public class PropertyService(
        IRepository repository,
        IFacilityService facilityService) : IPropertyService
    {
        public async Task<IEnumerable<PropertyInAllDto>> GetAllAsync(PropertyQueryParamsDto dto)
        {
            var propertiesDataAsQuery = repository
                .AllAsNoTracking<Property>()
                .Include(p => p.Facilities)
                .Include(p => p.Photos)
                .Where(p => p.IsDeleted == false);

            if (dto.PropertyTypeIds?.Any() ?? false)
            {
                propertiesDataAsQuery = propertiesDataAsQuery
                    .Where(p => dto.PropertyTypeIds.Contains(p.PropertyTypeId));
            }

            if (dto.FacilityIds?.Any() ?? false)
            {
                /*propertiesDataAsQuery = propertiesDataAsQuery
                    .Where(p => dto.FacilityIds.All(fi => p.Facilities.Any(f => f.FacilityId == fi)));*/

                var facilityIdsCount = dto.FacilityIds.Count();

                propertiesDataAsQuery = propertiesDataAsQuery
                    .Where(p => p.Facilities
                        .Where(f => dto.FacilityIds.Contains(f.FacilityId))
                        .Select(f => f.FacilityId)
                        .Distinct()
                        .Count() == facilityIdsCount);
            }

            if (dto.AccessibilityIds?.Any() ?? false)
            {
                /*propertiesDataAsQuery = propertiesDataAsQuery
                    .Where(p => dto.AccessibilityIds.All(fi => p.Facilities.Any(f => f.FacilityId == fi)));*/

                var accessibilityIdsCount = dto.AccessibilityIds.Count();

                propertiesDataAsQuery = propertiesDataAsQuery
                    .Where(p => p.Facilities
                        .Where(f => dto.AccessibilityIds.Contains(f.FacilityId))
                        .Select(f => f.FacilityId)
                        .Distinct()
                        .Count() == accessibilityIdsCount);
            }

            var propertiesData = await propertiesDataAsQuery
                .Select(p => new PropertyInAllDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StarsCount = p.StarsCount,
                    Address = p.Address,
                    MainPhoto = p.Photos.Any() ? p.Photos.First().Url : string.Empty,
                    Accessibility = p.Facilities
                        .Where(f => f.Facility.IsForAccessibility)
                        .Select(f => f.Facility.Name),
                    PublisherId = p.PublisherId,
                })
                .ToListAsync();

            return propertiesData;
        }

        public async Task<PropertyDetailsDto?> GetByIdAsync(Guid id)
            => await repository
                .AllAsNoTracking<Property>()
                .Where(p => p.Id == id && p.IsDeleted == false)
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

        public async Task<IEnumerable<UserPropertyDto>> GetByUserIdAsync(Guid userId)
            => await repository
                .AllAsNoTracking<Property>()
                .Where(p => p.PublisherId == userId && p.IsDeleted == false)
                .Select(p => new UserPropertyDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StarsCount = p.StarsCount,
                    Address = p.Address,
                    Description = p.Description,
                    MainPhoto = p.Photos.First().Url,
                    PublisherId = p.PublisherId,
                })
                .ToListAsync();

        public async Task<bool> HasPropertyWithGivenIdAsync(Guid id)
            => await repository
                .AllAsNoTracking<Property>()
                .AnyAsync(p => p.Id == id && p.IsDeleted == false);

        public async Task<bool> IsUserPropertyPublisherAsync(Guid propertyId, Guid userId)
            => await repository
                .AllAsNoTracking<Property>()
                .AnyAsync(p => p.Id == propertyId && p.PublisherId == userId && p.IsDeleted == false);

        public async Task<Guid> CreateAsync(PropertyInputDto dto, Guid userId)
        {
            var validSelectedFacilityIds = await facilityService.GetValidSelectedIdsAsync(dto.FacilityIds);
            var validImageUrls = GetValidImageUrls(dto.ImageUrls);

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
                Facilities = validSelectedFacilityIds.Select(vsfi => new PropertyFacility() { FacilityId = vsfi}).ToList(),
                Photos = validImageUrls.Select(viu => new PropertyPhoto() { Url = viu}).ToList()
            };

            await repository.AddAsync(newProperty);
            await repository.SaveChangesAsync();

            return newProperty.Id;
        }
        public async Task<Guid> EditAsync(Guid id, PropertyInputDto dto)
        {
            var propertyToEdit = await repository
                .All<Property>()
                .Include(p => p.Facilities)
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(p => p.Id == id);

            propertyToEdit!.Name = dto.Name;
            propertyToEdit.StarsCount = dto.StarsCount;
            propertyToEdit.CheckIn = (TimeOnly)dto.CheckIn!;
            propertyToEdit.CheckOut = (TimeOnly)dto.CheckOut!;
            propertyToEdit.Address = dto.Address;
            propertyToEdit.Description = dto.Description;
            propertyToEdit.PropertyTypeId = (int)dto.TypeId!;
            propertyToEdit.UpdatedAt = DateTime.UtcNow;

            var (facilitiesToAdd, facilitiesToRemove) = await GetFacilitiesToAddAndToRemoveAsync(propertyToEdit, dto.FacilityIds);
            var (photosToAdd, photosToRemove) = GetPhotosToAddAndToRemove(propertyToEdit, dto.ImageUrls);

            repository.RemoveRange(facilitiesToRemove);
            await repository.AddRangeAsync(facilitiesToAdd);

            repository.RemoveRange(photosToRemove);
            await repository.AddRangeAsync(photosToAdd);

            await repository.SaveChangesAsync();

            return propertyToEdit.Id;
        }

        private async Task<(IEnumerable<PropertyFacility> facilitiesToAdd, IEnumerable<PropertyFacility> facilitiesToRemove)> GetFacilitiesToAddAndToRemoveAsync(
            Property propertyToEdit,
            IEnumerable<int?> selectedFacilityIds)
        {
            var validSelectedFacilityIds = await facilityService.GetValidSelectedIdsAsync(selectedFacilityIds);

            var facilitiesToAdd = validSelectedFacilityIds
                .Except(propertyToEdit.Facilities.Select(f => f.FacilityId))
                .Select(vsfi => new PropertyFacility()
                {
                    PropertyId = propertyToEdit.Id,
                    FacilityId = vsfi,
                })
                .ToList();

            var facilitiesToRemove = propertyToEdit.Facilities
                .Where(f => validSelectedFacilityIds.Contains(f.FacilityId) == false)
                .ToList();

            return (facilitiesToAdd, facilitiesToRemove);
        }

        private (IEnumerable<PropertyPhoto> photosToAdd, IEnumerable<PropertyPhoto> photosToRemove) GetPhotosToAddAndToRemove(
            Property propertyToEdit,
            IEnumerable<string> imageUrls)
        {
            var validImageUrls = GetValidImageUrls(imageUrls);

            var photosToRemove = propertyToEdit.Photos
                .Where(p => validImageUrls.Contains(p.Url, StringComparer.OrdinalIgnoreCase) == false)
                .ToList();

            var photosToAdd = validImageUrls
                .Except(propertyToEdit.Photos.Select(p => p.Url), StringComparer.OrdinalIgnoreCase)
                .Select(viu => new PropertyPhoto()
                {
                    Url = viu,
                    PropertyId = propertyToEdit.Id,
                })
                .ToList();

            return (photosToAdd, photosToRemove);
        }
    }
}
