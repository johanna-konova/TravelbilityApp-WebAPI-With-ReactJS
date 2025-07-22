using TravelbilityApp.Core.DTOs.Room;

namespace TravelbilityApp.Core.Contracts
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomTypeOptionDto>> GetAllAsync();
        Task<bool> HasRoomTypeWithGivenIdAsync(int? id);
        Task<bool> IsRoomAccessibleAsync(int? id);
    }
}
