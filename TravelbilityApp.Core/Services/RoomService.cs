using Microsoft.EntityFrameworkCore;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Facility;
using TravelbilityApp.Core.DTOs.Room;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;
using TravelbilityApp.Infrastructure.Data.Models.Enums;

using static TravelbilityApp.Core.CommonHelpers;

namespace TravelbilityApp.Core.Services
{
    public class RoomService(
        IRepository repository,
        IFacilityService facilityService,
        IPropertyService propertyService) : IRoomService
    {
        public async Task<IEnumerable<RoomShortInfoDto>> GetAllByPropertyIdAsync(Guid propertyId)
            => await repository
                .AllAsNoTracking<Room>()
                .Where(r => r.PropertyId == propertyId && r.IsDeleted == false)
                .Select(r => new RoomShortInfoDto()
                {
                    Id = r.Id,
                    RoomTypeName = r.RoomType.Name,
                    MainBedTypeName = r.MainBedType.Name,
                    PricePerNight = r.PricePerNight,
                    MainPhotoUrl = r.Photos.Any()
                        ? r.Photos.First().Url
                        : string.Empty,
                    IsAccessibleRoom = r.RoomType.IsForAccessibility,
                })
                .ToListAsync();

        public async Task<IEnumerable<RoomShortDetailsDto>> GetAllDetailedByPropertyIdAsync(Guid propertyId)
            => await repository
                .AllAsNoTracking<Room>()
                .Where(r => r.PropertyId == propertyId && r.IsDeleted == false)
                .Select(r => new RoomShortDetailsDto()
                {
                    Id = r.Id,
                    RoomTypeName = r.RoomType.Name,
                    MainBedTypeName = r.MainBedType.Name,
                    MaxGuests = r.MaxGuests,
                    Size = r.SizeInSquareMeters,
                    PricePerNight = r.PricePerNight,
                    IsAccessibleRoom = r.RoomType.IsForAccessibility,
                    CommonFacilityNames = r.Facilities
                        .Where(f => f.Facility.IsForAccessibility == false)
                        .Select(f => f.Facility.Name),
                    AccessibilityNames = r.Facilities
                        .Where(f => f.Facility.IsForAccessibility == true)
                        .Select(f => f.Facility.Name),
                })
                .ToListAsync();

        public async Task<RoomDetailsDto> GetByIdAndPropertyIdAsync(Guid roomId, Guid propertyId)
            => await repository
                .AllAsNoTracking<Room>()
                .Where(r => r.Id == roomId && r.PropertyId == propertyId && r.IsDeleted == false)
                .Select(r => new RoomDetailsDto()
                {
                    Id = r.Id,
                    RoomTypeName = r.RoomType.Name,
                    MainBedTypeName = r.MainBedType.Name,
                    PricePerNight = r.PricePerNight,
                    MaxGuests = r.MaxGuests,
                    Size = r.SizeInSquareMeters,
                    NumberOfUnits = r.NumberOfUnits,
                    Description = r.Description,
                    IsAccessibleRoom = r.RoomType.IsForAccessibility,
                    CommonFacilityNames = r.Facilities
                        .Where(f => f.Facility.IsForAccessibility == false)
                        .Select(f => f.Facility.Name),
                    AccessibilityNames = r.Facilities
                        .Where(f => f.Facility.IsForAccessibility == true)
                        .Select(f => f.Facility.Name),
                    PhotoUrls = r.Photos
                        .Select(p => p.Url),
                })

                .SingleAsync();

        public async Task<RoomForEditDto> GetForEditByIdAndPropertyIdAsync(Guid roomId, Guid propertyId)
            => await repository
                .AllAsNoTracking<Room>()
                .Where(r => r.Id == roomId && r.PropertyId == propertyId && r.IsDeleted == false)
                .Select(r => new RoomForEditDto()
                {
                    Id = r.Id,
                    RoomType = new RoomTypeOptionDto()
                    {
                        Id = r.RoomType.Id,
                        Name = r.RoomType.Name,
                        IsForAccessibility = r.RoomType.IsForAccessibility,
                    },
                    MainBedType = new RoomBedTypeOptionDto()
                    {
                        Id = r.MainBedType.Id,
                        Name = r.MainBedType.Name,
                    },
                    PricePerNight = r.PricePerNight,
                    MaxGuests = r.MaxGuests,
                    Size = r.SizeInSquareMeters,
                    NumberOfUnits = r.NumberOfUnits,
                    Description = r.Description,
                    Facilities = r.Facilities
                        .Select(f => new FacilityOptionDto()
                        {
                            Id = f.Facility.Id,
                            Name = f.Facility.Name,
                            IsForAccessibility = f.Facility.IsForAccessibility,
                        }),
                    PhotoUrls = r.Photos
                        .Select(p => p.Url),
                })

                .SingleAsync();

        public async Task<bool> HasRoomWithGivenIdAsync(Guid id)
            => await repository
                .AllAsNoTracking<Room>()
                .AnyAsync(r => r.Id == id && r.IsDeleted == false);

        public async Task<RoomShortInfoDto> CreateAsync(RoomInputDto dto)
        {
            var validSelectedFacilityIds = await facilityService.GetValidSelectedIdsAsync(dto.FacilityIds, WhereStatus.OnlyInRoom);
            var validImageUrls = GetValidImageUrls(dto.PhotoUrls);

            var newRoom = new Room()
            {
                RoomTypeId = (int)dto.RoomTypeId!,
                MaxGuests = (int)dto.MaxGuests!,
                PricePerNight = (decimal)dto.PricePerNight!,
                MainBedTypeId = (int)dto.MainBedTypeId!,
                SizeInSquareMeters = (double)dto.Size!,
                NumberOfUnits = (int)dto.NumberOfUnits!,
                Description = dto.Description,
                PropertyId = dto.PropertyId,
            };

            var roomFacilities = validSelectedFacilityIds
                .Select(facilityId => new PropertyFacility
                {
                    PropertyId = dto.PropertyId,
                    RoomId = newRoom.Id,
                    FacilityId = facilityId
                });

            var roomPhotos = validImageUrls
                .Select(url => new PropertyPhoto
                {
                    PropertyId = dto.PropertyId,
                    RoomId = newRoom.Id,
                    Url = url,
                });

            await repository.AddAsync(newRoom);
            await repository.AddRangeAsync(roomFacilities);
            await repository.AddRangeAsync(roomPhotos);

            await repository.SaveChangesAsync();

            return await GetByIdAsync(newRoom.Id);
        }

        public async Task<RoomShortInfoDto> EditAsync(Guid id, RoomInputDto dto)
        {
            await using var transaction = await repository.BeginTransactionAsync();

            try
            {
                var roomToEdit = await repository
                    .All<Room>()
                    .Include(r => r.RoomType)
                    .Include(r => r.Facilities)
                    .Include(r => r.Photos)
                    .SingleAsync(r => r.Id == id && r.IsDeleted == false);

                var previousRoomType = roomToEdit.RoomType;

                roomToEdit.RoomTypeId = (int)dto.RoomTypeId!;
                roomToEdit.MaxGuests = (int)dto.MaxGuests!;
                roomToEdit.PricePerNight = (decimal)dto.PricePerNight!;
                roomToEdit.MainBedTypeId = (int)dto.MainBedTypeId!;
                roomToEdit.SizeInSquareMeters = (double)dto.Size!;
                roomToEdit.NumberOfUnits = (int)dto.NumberOfUnits!;
                roomToEdit.Description = dto.Description;

                var validSelectedFacilityIds = await facilityService.GetValidSelectedIdsAsync(dto.FacilityIds, WhereStatus.OnlyInRoom);

                var (facilitiesToAdd, facilitiesToRemove) = GetFacilitiesToAddAndToRemoveAsync(roomToEdit.Facilities, validSelectedFacilityIds, roomToEdit.PropertyId, roomToEdit.Id);

                var (photosToAdd, photosToRemove) = GetPhotosToAddAndToRemove(roomToEdit.Photos, dto.PhotoUrls, roomToEdit.PropertyId, roomToEdit.Id);

                repository.RemoveRange(facilitiesToRemove);
                await repository.AddRangeAsync(facilitiesToAdd);

                repository.RemoveRange(photosToRemove);
                await repository.AddRangeAsync(photosToAdd);

                await repository.SaveChangesAsync();

                await SetSavedPropertyStatusIfNoAccessibleRoomsAsync(previousRoomType.IsForAccessibility, roomToEdit.PropertyId);

                await transaction.CommitAsync();

                return await GetByIdAsync(id);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await using var transaction = await repository.BeginTransactionAsync();

            try
            {
                var roomToDelete = await repository
                    .All<Room>()
                    .Where(r => r.Id == id && r.IsDeleted == false)
                    .Include(r => r.RoomType)
                    .SingleOrDefaultAsync();

                if (roomToDelete == null)
                {
                    await transaction.RollbackAsync();
                    return;
                }

                roomToDelete.IsDeleted = true;
                await repository.SaveChangesAsync();

                await SetSavedPropertyStatusIfNoAccessibleRoomsAsync(roomToDelete.RoomType.IsForAccessibility, roomToDelete.PropertyId);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        private async Task SetSavedPropertyStatusIfNoAccessibleRoomsAsync(bool wasAccessibleRoom, Guid propertyId)
        {
            if (wasAccessibleRoom &&
                await propertyService.HasAccessibleRoom(propertyId) == false)
            {
                await propertyService.SaveByIdAsync(propertyId);
            }
        }


        private async Task<RoomShortInfoDto> GetByIdAsync(Guid id)
            => await repository
                .AllAsNoTracking<Room>()
                .Where(r => r.Id == id && r.IsDeleted == false)
                .Select(r => new RoomShortInfoDto()
                {
                    Id = r.Id,
                    RoomTypeName = r.RoomType.Name,
                    MainBedTypeName = r.MainBedType.Name,
                    PricePerNight = r.PricePerNight,
                    MainPhotoUrl = r.Photos.Any()
                        ? r.Photos.First().Url
                        : string.Empty,
                    IsAccessibleRoom = r.RoomType.IsForAccessibility,
                })
                .SingleAsync();
    }
}
