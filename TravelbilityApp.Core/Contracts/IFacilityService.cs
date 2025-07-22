using TravelbilityApp.Core.DTOs.Facility;
using TravelbilityApp.Infrastructure.Data.Models.Enums;

namespace TravelbilityApp.Core.Contracts
{
    public interface IFacilityService
    {
        Task<IEnumerable<FacilityOptionDto>> GetAllAsync(
            WhereStatus whereStatus = WhereStatus.OnlyInCommonArea);

        Task<IEnumerable<FacilityOptionDto>> GetValidSelectedAsync(
            IEnumerable<int?> selectedIds,
            WhereStatus whereStatus = WhereStatus.OnlyInCommonArea);
        
        Task<IEnumerable<int>> GetValidSelectedIdsAsync(
            IEnumerable<int?> selectedIds,
            WhereStatus whereStatus = WhereStatus.OnlyInCommonArea);

        Task<IEnumerable<FacilityOptionDto>> GetAccessibilityAsync();
    }
}
