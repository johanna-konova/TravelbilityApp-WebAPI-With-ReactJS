using Microsoft.EntityFrameworkCore;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Facility;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;
using TravelbilityApp.Infrastructure.Data.Models.Enums;

namespace TravelbilityApp.Core.Services
{
    public class FacilityService(IRepository repository) : IFacilityService
    {
        public async Task<IEnumerable<FacilityOptionDto>> GetAllAsync()
            => await repository
                .AllAsNoTracking<Facility>()
                .Select(f => new FacilityOptionDto()
                {
                    Id = f.Id,
                    Name = f.Name,
                    IsForAccessibility = f.IsForAccessibility,
                    WhereStatus = f.WhereStatus.ToString(),
                })
                .ToListAsync();

        public async Task<IEnumerable<FacilityOptionDto>> GetAllInAsync(WhereStatus whereStatus)
            => await repository
                .AllAsNoTracking<Facility>()
                .Where(f => f.WhereStatus == whereStatus || f.WhereStatus == WhereStatus.Both)
                .Select(f => new FacilityOptionDto()
                {
                    Id = f.Id,
                    Name = f.Name,
                    IsForAccessibility = f.IsForAccessibility,
                })
                .ToListAsync();

        public async Task<IEnumerable<FacilityOptionDto>> GetValidSelectedAsync(
            IEnumerable<int?> rawSelectedIds,
            WhereStatus whereStatus)
        {
            var selectedIds = rawSelectedIds
                .Where(rsi => rsi.HasValue)
                .Distinct();

            return await repository
                .AllAsNoTracking<Facility>()
                .Where(f => (f.WhereStatus == whereStatus || f.WhereStatus == WhereStatus.Both) && selectedIds.Contains(f.Id))
                .Select(f => new FacilityOptionDto()
                {
                    Id = f.Id,
                    Name = f.Name,
                    IsForAccessibility = f.IsForAccessibility,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetValidSelectedIdsAsync(
            IEnumerable<int?> selectedIds,
            WhereStatus whereStatus)
            => await repository
                .AllAsNoTracking<Facility>()
                .Where(f => (f.WhereStatus == whereStatus || f.WhereStatus == WhereStatus.Both) && selectedIds.Contains(f.Id))
                .Select(f => f.Id)
                .ToListAsync();

        public async Task<IEnumerable<FacilityOptionDto>> GetAccessibilityAsync()
            => await repository
                .AllAsNoTracking<Facility>()
                .Where(f => f.IsForAccessibility &&
                            (f.WhereStatus == WhereStatus.OnlyInCommonArea ||
                             f.WhereStatus == WhereStatus.Both))
                .Select(f => new FacilityOptionDto()
                {
                    Id = f.Id,
                    Name = f.Name,
                    IsForAccessibility = f.IsForAccessibility,
                })
                .ToListAsync();
    }
}
