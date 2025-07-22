using Microsoft.EntityFrameworkCore;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Facility;
using TravelbilityApp.Core.DTOs.Property;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;
using TravelbilityApp.Infrastructure.Data.Models.Enums;

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
                .Where(p => p.Status == PropertyStatus.Published);

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

            var mainPhotoUrl = propertiesDataAsQuery
                .Select(p => p.Photos
                        .Where(p => p.RoomId == null));

            var propertiesData = await propertiesDataAsQuery
                .Select(p => new PropertyInAllDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StarsCount = p.StarsCount,
                    Address = p.Address,
                    MainPhotoUrl = p.Photos
                        .Where(p => p.RoomId == null)
                        .First().Url,
                    Accessibility = p.Facilities
                        .Where(f => f.Facility.IsForAccessibility)
                        .Select(f => f.Facility.Name),
                    PublisherId = p.PublisherId,
                })
                .ToListAsync();

            return propertiesData;
        }

        public async Task<IEnumerable<PropertyInNewestAddedDto>> GetNewestAddedAsync(int count)
            => await repository
                .AllAsNoTracking<Property>()
                .Where(p => p.Status == PropertyStatus.Published)
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .Select(p => new PropertyInNewestAddedDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StarsCount = p.StarsCount,
                    Address = p.Address,
                    TypeName = p.PropertyType.Name,
                    MainPhotoUrl = p.Photos
                        .Where(p => p.RoomId == null)
                        .First().Url,
                    PublisherId = p.PublisherId,
                })
                .ToListAsync();

        public async Task<IEnumerable<UserPropertyDto>> GetByUserIdAsync(Guid userId)
            => await repository
                .AllAsNoTracking<Property>()
                .Where(p => p.PublisherId == userId && p.Status != PropertyStatus.Deleted)
                .Select(p => new UserPropertyDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StarsCount = p.StarsCount,
                    Address = p.Address,
                    Description = p.Description,
                    MainPhotoUrl = p.Photos
                        .Where(p => p.RoomId == null)
                        .First().Url,
                    Status = p.Status.ToString(),
                    PublisherId = p.PublisherId,
                })
                .ToListAsync();

        public async Task<PropertyDetailsDto> GetByIdAsync(Guid id, PropertyStatus status)
            => await repository
                .AllAsNoTracking<Property>()
                .Where(p => p.Id == id && p.Status >= status)
                .Select(p => new PropertyDetailsDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    StarsCount = p.StarsCount,
                    CheckIn = p.CheckIn,
                    CheckOut = p.CheckOut,
                    Address = p.Address,
                    Description = p.Description,
                    Status = p.Status.ToString(),
                    PublisherId = p.PublisherId,
                    Type = new PropertyTypeOptionDto()
                    {
                        Id = p.PropertyType.Id,
                        Name = p.PropertyType.Name,
                    },
                    Facilities = p.Facilities
                        .Where(f => f.RoomId == null)
                        .Select(f => new FacilityOptionDto()
                        {
                            Id = f.FacilityId,
                            Name = f.Facility.Name,
                            IsForAccessibility = f.Facility.IsForAccessibility,
                        }),
                    PhotoUrls = p.Photos
                        .Where(p => p.RoomId == null)
                        .Select(p => p.Url)
                })
                .SingleAsync();

        public async Task<bool> IsUserPropertyPublisherAsync(Guid propertyId, Guid userId)
            => await repository
                .AllAsNoTracking<Property>()
                .AnyAsync(p => p.Id == propertyId && p.PublisherId == userId && p.Status >= PropertyStatus.Saved);

        public async Task<bool> HasPropertyWithGivenIdAsync(Guid id, PropertyStatus status)
            => await repository
                .AllAsNoTracking<Property>()
                .AnyAsync(p => p.Id == id && p.Status >= status);

        public async Task<bool> HasAccessibleRoom(Guid id)
            => await repository
                .AllAsNoTracking<Property>()
                .Where(p => p.Id == id)
                .AnyAsync(p => p.Rooms.Any(r => r.IsDeleted == false && r.RoomType.IsForAccessibility == true));

        public async Task<Guid> CreateAsync(PropertyInputDto dto, Guid userId)
        {
            var validSelectedFacilityIds = await facilityService.GetValidSelectedIdsAsync(dto.FacilityIds);
            var validImageUrls = GetValidImageUrls(dto.PhotoUrls);

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
                Facilities = validSelectedFacilityIds.Select(vsfi => new PropertyFacility() { FacilityId = vsfi }).ToList(),
                Photos = validImageUrls.Select(viu => new PropertyPhoto() { Url = viu }).ToList()
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

            var validSelectedFacilityIds = await facilityService.GetValidSelectedIdsAsync(dto.FacilityIds);

            var (facilitiesToAdd, facilitiesToRemove) = GetFacilitiesToAddAndToRemoveAsync(
                propertyToEdit.Facilities.Where(f => f.RoomId == null),
                validSelectedFacilityIds,
                propertyToEdit.Id);

            var (photosToAdd, photosToRemove) = GetPhotosToAddAndToRemove(
                propertyToEdit.Photos.Where(p => p.RoomId == null),
                dto.PhotoUrls,
                propertyToEdit.Id);

            repository.RemoveRange(facilitiesToRemove);
            await repository.AddRangeAsync(facilitiesToAdd);

            repository.RemoveRange(photosToRemove);
            await repository.AddRangeAsync(photosToAdd);

            await repository.SaveChangesAsync();

            return propertyToEdit.Id;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var propertyToDelete = await repository
                .GetByIdAsync<Property>(id);

            propertyToDelete!.Status = PropertyStatus.Deleted;

            await repository.SaveChangesAsync();
        }

        public async Task PublishByIdAsync(Guid id)
        {
            var propertyToPublish = await repository
                .GetByIdAsync<Property>(id);

            propertyToPublish!.Status = PropertyStatus.Published;

            await repository.SaveChangesAsync();
        }

        public async Task SaveByIdAsync(Guid id)
        {
            var propertyToSave = await repository
                .GetByIdAsync<Property>(id);

            if (propertyToSave == null ||
                propertyToSave.Status != PropertyStatus.Published)
            {
                return;
            }

            propertyToSave.Status = PropertyStatus.Saved;

            await repository.SaveChangesAsync();
        }
    }
}
