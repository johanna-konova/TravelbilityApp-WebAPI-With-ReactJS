using TravelbilityApp.Core.DTOs.Property;

namespace TravelbilityApp.Core.Contracts
{
    public interface IPropertyService
    {
        Task<Guid> CreateAsync(CreatePropertyDto dto, Guid userId);
    }
}
