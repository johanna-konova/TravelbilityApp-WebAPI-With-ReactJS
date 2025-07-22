using Microsoft.EntityFrameworkCore;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Room;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;

namespace TravelbilityApp.Core.Services
{
    public class RoomTypeService(IRepository repository) : IRoomTypeService
    {
        public async Task<IEnumerable<RoomTypeOptionDto>> GetAllAsync()
            => await repository
                .AllAsNoTracking<RoomType>()
                .Select(rt => new RoomTypeOptionDto()
                {
                    Id = rt.Id,
                    Name = rt.Name,
                    IsForAccessibility = rt.IsForAccessibility,
                })
                .ToListAsync();

        public async Task<bool> HasRoomTypeWithGivenIdAsync(int? id)
            => await repository
                .AllAsNoTracking<RoomType>()
                .AnyAsync(rt => rt.Id == id);

        public async Task<bool> IsRoomAccessibleAsync(int? id)
            => await repository
                .AllAsNoTracking<RoomType>()
                .AnyAsync(rt => rt.Id == id && rt.IsForAccessibility == true);
    }
}
