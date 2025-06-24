using TravelbilityApp.Core.DTOs.Property;

namespace TravelbilityApp.Core.Contracts
{
    public interface IPropertyService
    {
        Task<PropertyDetailsDto> GetByIdAsync(Guid id);
        Task<IEnumerable<UserPropertyDto>> GetByUserIdAsync(Guid userId);
        Task<bool> HasPropertyWithGivenIdAsync(Guid id);
        Task<bool> IsUserPropertyPublisherAsync(Guid propertyId, Guid userId);
        Task<Guid> CreateAsync(PropertyInputDto dto, Guid userId);
        Task<Guid> EditAsync(Guid id, PropertyInputDto dto);
    }
}
