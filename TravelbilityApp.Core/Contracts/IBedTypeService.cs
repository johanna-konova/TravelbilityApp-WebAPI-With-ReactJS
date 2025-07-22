using TravelbilityApp.Core.DTOs.Room;

namespace TravelbilityApp.Core.Contracts
{
    public interface IBedTypeService
    {
        Task<IEnumerable<RoomBedTypeOptionDto>> GetAllAsync();
        Task<bool> HasBedTypeWithGivenIdAsync(int? id);
    }
}
