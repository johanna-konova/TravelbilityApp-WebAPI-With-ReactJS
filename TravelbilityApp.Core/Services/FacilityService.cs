using Microsoft.EntityFrameworkCore;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Property;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data.Models;

namespace TravelbilityApp.Core.Services
{
    public class FacilityService(IRepository repository) : IFacilityService
    {
        public async Task<IEnumerable<PropertyFacilityOptionDto>> GetAllAsync()
            => await repository
                .AllAsNoTracking<Facility>()
                .Select(f => new PropertyFacilityOptionDto()
                {
                    Id = f.Id,
                    Name = f.Name,
                    IsForAccessibility = f.IsForAccessibility,
                })
                .ToListAsync();

        public async Task<IEnumerable<PropertyFacilityOptionDto>> GetValidSelectedAsync(
            IEnumerable<int?> rawSelectedIds)
        {
            var selectedIds = rawSelectedIds
                .Where(rsi => rsi.HasValue)
                .Distinct();

            return await repository
                .AllAsNoTracking<Facility>()
                .Where(f => selectedIds.Contains(f.Id))
                .Select(f => new PropertyFacilityOptionDto()
                {
                    Id = f.Id,
                    Name = f.Name,
                    IsForAccessibility = f.IsForAccessibility,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetValidSelectedIdsAsync(IEnumerable<int?> selectedIds)
            => await repository
                .AllAsNoTracking<Facility>()
                .Where(f => selectedIds.Contains(f.Id))
                .Select(f => f.Id)
                .ToListAsync();

        public async Task<IEnumerable<PropertyFacilityOptionDto>> GetAccessibilityAsync()
            => await repository
                .AllAsNoTracking<Facility>()
                .Where(f => f.IsForAccessibility)
                .Select(f => new PropertyFacilityOptionDto()
                {
                    Id = f.Id,
                    Name = f.Name,
                    IsForAccessibility = f.IsForAccessibility,
                })
                .ToListAsync();
    }
}
