using TravelbilityApp.Core.DTOs.Property;

namespace TravelbilityApp.Core.Contracts
{
    public interface IFacilityService
    {
        Task<IEnumerable<PropertyFacilityOptionDto>> GetAllAsync();
    }
}
