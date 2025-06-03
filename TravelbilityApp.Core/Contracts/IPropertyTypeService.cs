using TravelbilityApp.Core.DTOs.Property;

namespace TravelbilityApp.Core.Contracts
{
    public interface IPropertyTypeService
    {
        Task<IEnumerable<PropertyTypeOptionDto>> GetAllAsync();
    }
}
