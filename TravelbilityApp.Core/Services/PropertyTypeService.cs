using Microsoft.EntityFrameworkCore;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Infrastructure.Data.Models;
using TravelbilityApp.Core.DTOs.Property;
using TravelbilityApp.Infrastructure.Common;

namespace TravelbilityApp.Core.Services
{
    public class PropertyTypeService(IRepository repository) : IPropertyTypeService
    {
        public async Task<IEnumerable<PropertyTypeOptionDto>> GetAllAsync()
            => await repository
                .AllAsNoTracking<PropertyType>()
                .Select(pt => new PropertyTypeOptionDto()
                {
                    Id = pt.Id,
                    Name = pt.Name,
                })
                .ToListAsync();
    }
}
