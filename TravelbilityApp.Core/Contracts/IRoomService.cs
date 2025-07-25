using TravelbilityApp.Core.DTOs.Room;

namespace TravelbilityApp.Core.Contracts
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomShortInfoDto>> GetAllByPropertyIdAsync(Guid propertyId);
        Task<IEnumerable<RoomShortDetailsDto>> GetAllDetailedByPropertyIdAsync(Guid propertyId);
        Task<RoomDetailsDto> GetByIdAndPropertyIdAsync(Guid roomId, Guid propertyId);
        Task<RoomForEditDto> GetForEditByIdAndPropertyIdAsync(Guid roomId, Guid propertyId);
        Task<bool> HasRoomWithGivenIdAsync(Guid id);
        Task<RoomShortInfoDto> CreateAsync(RoomInputDto dto);
        Task<RoomShortInfoDto> EditAsync(Guid id, RoomInputDto dto);
        Task DeleteByIdAsync(Guid roomId);
    }
}
