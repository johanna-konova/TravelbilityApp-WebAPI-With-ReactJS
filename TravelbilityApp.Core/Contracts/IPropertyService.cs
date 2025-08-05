using TravelbilityApp.Core.DTOs.Common;
using TravelbilityApp.Core.DTOs.Property;
using TravelbilityApp.Infrastructure.Data.Models.Enums;

namespace TravelbilityApp.Core.Contracts
{
    public interface IPropertyService
    {
        Task<PagedResultDto<PropertyInAllDto>> GetAllAsync(PropertyQueryParamsDto dto);
        Task<IEnumerable<PropertyInNewestAddedDto>> GetNewestAddedAsync(int count);
        Task<PagedResultDto<UserPropertyDto>> GetAllByUserIdAsync(Guid userId, int currenPageNumber, int propertiesPerPage = 1);
        Task<PropertyDetailsDto> GetByIdAsync(Guid id, PropertyStatus status = PropertyStatus.Saved);
        Task<UserPropertyDetailsDto> GetByUserIdAsync(Guid id, Guid userId, PropertyStatus status = PropertyStatus.Saved);
        Task<PropertyForEditDto> GetForEditByIdAsync(Guid id, Guid userId, PropertyStatus status = PropertyStatus.Saved);
        Task<PagedResultDto<PropertForAdminShortDto>> GetAllForAdminAsync(int currenPageNumber, int propertiesPerPage = 1);
        Task<bool> IsUserPropertyPublisherAsync(Guid propertyId, Guid userId);
        Task<bool> HasPropertyWithGivenIdAsync(Guid id, PropertyStatus status = PropertyStatus.Saved);
        Task<bool> HasAccessibleRoom(Guid id);
        Task<Guid> CreateAsync(PropertyInputDto dto, Guid userId);
        Task<Guid> EditAsync(Guid id, PropertyInputDto dto);
        Task ChangePropertyStatus(Guid id, PropertyStatus newStatus);
    }
}
