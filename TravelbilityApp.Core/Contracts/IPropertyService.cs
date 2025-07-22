using TravelbilityApp.Core.DTOs.Property;
using TravelbilityApp.Infrastructure.Data.Models.Enums;

namespace TravelbilityApp.Core.Contracts
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyInAllDto>> GetAllAsync(PropertyQueryParamsDto dto);
        Task<IEnumerable<PropertyInNewestAddedDto>> GetNewestAddedAsync(int count);
        Task<IEnumerable<UserPropertyDto>> GetByUserIdAsync(Guid userId);
        Task<PropertyDetailsDto> GetByIdAsync(Guid id, PropertyStatus status = PropertyStatus.Saved);
        Task<bool> IsUserPropertyPublisherAsync(Guid propertyId, Guid userId);
        Task<bool> HasPropertyWithGivenIdAsync(Guid id, PropertyStatus status = PropertyStatus.Saved);
        Task<bool> HasAccessibleRoom(Guid id);
        Task<Guid> CreateAsync(PropertyInputDto dto, Guid userId);
        Task<Guid> EditAsync(Guid id, PropertyInputDto dto);
        Task DeleteByIdAsync(Guid id);
        Task SaveByIdAsync(Guid propertyId);
        Task PublishByIdAsync(Guid id);
    }
}
