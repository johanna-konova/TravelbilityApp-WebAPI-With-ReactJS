using TravelbilityApp.Core.DTOs.Property;

namespace TravelbilityApp.Core.Contracts
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyInAllDto>> GetAllAsync(PropertyQueryParamsDto dto);
        Task<IEnumerable<PropertyInNewestAddedDto>> GetNewestAddedAsync(int count);
        Task<IEnumerable<UserPropertyDto>> GetByUserIdAsync(Guid userId);
        Task<PropertyDetailsDto> GetByIdAsync(Guid id);
        Task<bool> HasPropertyWithGivenIdAsync(Guid id);
        Task<bool> IsUserPropertyPublisherAsync(Guid propertyId, Guid userId);
        Task<Guid> CreateAsync(PropertyInputDto dto, Guid userId);
        Task<Guid> EditAsync(Guid id, PropertyInputDto dto);
        Task DeleteByIdAsync(Guid id);
    }
}
