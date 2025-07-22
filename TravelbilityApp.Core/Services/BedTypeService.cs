using Microsoft.EntityFrameworkCore;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Room;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;

namespace TravelbilityApp.Core.Services
{
    public class BedTypeService(IRepository repository) : IBedTypeService
    {
        public async Task<IEnumerable<RoomBedTypeOptionDto>> GetAllAsync()
            => await repository
                .AllAsNoTracking<BedType>()
                .Select(bt => new RoomBedTypeOptionDto()
                {
                    Id = bt.Id,
                    Name = bt.Name,
                })
                .ToListAsync();

        public async Task<bool> HasBedTypeWithGivenIdAsync(int? id)
            => await repository
                .AllAsNoTracking<BedType>()
                .AnyAsync(bt => bt.Id == id);
    }
}
