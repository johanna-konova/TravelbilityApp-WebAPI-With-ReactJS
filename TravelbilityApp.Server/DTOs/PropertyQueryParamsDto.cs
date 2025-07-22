using System.Web.Http.ModelBinding;
using TravelbilityApp.WebAPI.ModelBinders;

namespace TravelbilityApp.WebAPI.DTOs
{
    [ModelBinder(BinderType = typeof(CommaSeparatedModelBinder<int>))]
    public class PropertyQueryParamsDto
    {
        public IEnumerable<int>? PropertyTypeIds { get; init; }
        public IEnumerable<int>? FacilityIds { get; init; }
        public IEnumerable<int>? AccessibilityIds { get; init; }
    }
}
