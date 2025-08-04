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

            if (dto.RoomTypeIds?.Any() ?? false)
            {
                var roomTypeIds = dto.RoomTypeIds.ToList();

                propertiesDataAsQuery = propertiesDataAsQuery
                    .Where(p => p.Rooms
                         .Where(r =>r.IsDeleted == false && roomTypeIds.Contains(r.RoomTypeId))
                         .Select(r => r.RoomTypeId)
                         .Distinct()
                         .Count() == roomTypeIds.Count
                    );
            }

            propertiesDataAsQuery = FilterBySelectedFacilityIds(
                propertiesDataAsQuery,
                (dto.PropertyFacilityIds, true),
                (dto.PropertyAccessibilityIds, true),
                (dto.RoomFacilityIds, false),
                (dto.RoomAccessibilityIds, false)
            );

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
                    AccessibilityNames = p.Facilities
                        .Where(f => f.Facility.IsForAccessibility &&
                                    f.RoomId == null)
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

        public async Task<IEnumerable<UserPropertyDto>> GetAllByUserIdAsync(Guid userId)
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
                    Address = p.Address,
                    Description = p.Description,
                    PublisherId = p.PublisherId,
                    CommonFacilityNames = p.Facilities
                        .Where(f => f.RoomId == null &&
                               f.Facility.IsForAccessibility == false)
                        .Select(f => f.Facility.Name),
                    AccessibilityNames = p.Facilities
                        .Where(f => f.RoomId == null &&
                               f.Facility.IsForAccessibility == true)
                        .Select(f => f.Facility.Name),
                    PhotoUrls = p.Photos
                        .Where(p => p.RoomId == null)
                        .Select(p => p.Url)
                })
                .SingleAsync();

        public async Task<UserPropertyDetailsDto> GetByUserIdAsync(Guid id, Guid userId, PropertyStatus status)
            => await repository
                .AllAsNoTracking<Property>()
                .Where(p => p.Id == id && p.PublisherId == userId && p.Status >= status)
                .Select(p => new UserPropertyDetailsDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    TypeName = p.PropertyType.Name,
                    StarsCount = p.StarsCount,
                    CheckIn = p.CheckIn.ToString("HH:ss"),
                    CheckOut = p.CheckOut.ToString("HH:ss"),
                    Address = p.Address,
                    Description = p.Description,
                    PublisherId = p.PublisherId,
                    CommonFacilityNames = p.Facilities
                        .Where(f => f.RoomId == null &&
                               f.Facility.IsForAccessibility == false)
                        .Select(f => f.Facility.Name),
                    AccessibilityNames = p.Facilities
                        .Where(f => f.RoomId == null &&
                               f.Facility.IsForAccessibility == true)
                        .Select(f => f.Facility.Name),
                    PhotoUrls = p.Photos
                        .Where(p => p.RoomId == null)
                        .Select(p => p.Url)
                })
                .SingleAsync();

        public async Task<PropertyForEditDto> GetForEditByIdAsync(Guid id, Guid userId, PropertyStatus status)
            => await repository
                .AllAsNoTracking<Property>()
                .Where(p => p.Id == id && p.PublisherId == userId && p.Status >= status)
                .Select(p => new PropertyForEditDto()
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

        public async Task<IEnumerable<PropertForAdminShortDto>> GetAllForAdminAsync()
            => await repository
                .AllAsNoTracking<Property>()
                .Where(p => p.Status != PropertyStatus.Deleted)
                .Select(p => new PropertForAdminShortDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    TypeName = p.PropertyType.Name,
                    Address = p.Address,
                    Publisher = p.Publisher.Email!,
                    Status = p.Status.ToString(),
                })
                .ToListAsync();

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
            propertyToEdit.Status = PropertyStatus.Saved;
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

        public async Task ChangePropertyStatus(Guid id, PropertyStatus newStatus)
        {
            var property = await repository
                .GetByIdAsync<Property>(id);

            if (property != null &&
                property.Status != PropertyStatus.Deleted &&
                property.Status != newStatus)
            {
                property.Status = newStatus;

                await repository.SaveChangesAsync();
            }
        }

        private IQueryable<Property> FilterBySelectedFacilityIds(
            IQueryable<Property> propertiesDataAsQuery,
            params (IEnumerable<int>? SelectedFacilityIds, bool IsOnlyInCommomArea)[] filters)
        {
            foreach (var (selectedFacilityIds, isOnlyInCommomArea) in filters)
            {

                if (selectedFacilityIds == null ||
                    selectedFacilityIds.Any() == false)
                {
                    continue;
                }

                var materializedSelectedFacilityIds = selectedFacilityIds.ToList();

                propertiesDataAsQuery = propertiesDataAsQuery
                    .Where(p => p.Facilities
                        .Where(f => (isOnlyInCommomArea
                            ? f.RoomId == null
                            : f.RoomId != null && f.Room.IsDeleted == false) &&
                            materializedSelectedFacilityIds.Contains(f.FacilityId)
                            )
                        .Select(f => f.FacilityId)
                        .Distinct()
                        .Count() == materializedSelectedFacilityIds.Count
                    );
            }

            return propertiesDataAsQuery;
        }
    }
}
