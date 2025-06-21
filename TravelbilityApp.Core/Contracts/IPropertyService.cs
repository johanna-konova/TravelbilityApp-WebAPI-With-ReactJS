using TravelbilityApp.Core.DTOs.Property;

namespace TravelbilityApp.Core.Contracts
{
    public interface IPropertyService
    {
        Task<PropertyDetailsDto> GetByIdAsync(Guid id);
        Task<IEnumerable<UserPropertyDto>> GetByUserIdAsync(Guid userId);
        Task<Guid> CreateAsync(CreatePropertyDto dto, Guid userId);
    }
}
