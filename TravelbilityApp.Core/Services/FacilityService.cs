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
    }
}
