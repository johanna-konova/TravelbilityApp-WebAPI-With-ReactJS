using TravelbilityApp.Core.DTOs.Property;

namespace TravelbilityApp.Core.Contracts
{
    public interface IFacilityService
    {
        Task<IEnumerable<PropertyFacilityOptionDto>> GetAllAsync();
        Task<IEnumerable<PropertyFacilityOptionDto>> GetValidSelectedAsync(
            IEnumerable<int?> selectedIds);
        Task<IEnumerable<int>> GetValidSelectedIdsAsync(
            IEnumerable<int?> selectedIds);
        Task<IEnumerable<PropertyFacilityOptionDto>> GetAccessibilityAsync();
    }
}
