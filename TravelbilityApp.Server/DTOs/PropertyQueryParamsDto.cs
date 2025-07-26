using System.Web.Http.ModelBinding;
using TravelbilityApp.WebAPI.ModelBinders;

namespace TravelbilityApp.WebAPI.DTOs
{
    [ModelBinder(BinderType = typeof(CommaSeparatedModelBinder<int>))]
    public class PropertyQueryParamsDto
    {
        public IEnumerable<int>? PropertyTypeIds { get; init; }
        public IEnumerable<int>? RoomTypeIds { get; init; }
        public IEnumerable<int>? PropertyFacilityIds { get; init; }
        public IEnumerable<int>? RoomFacilityIds { get; init; }
        public IEnumerable<int>? PropertyAccessibilityIds { get; init; }
        public IEnumerable<int>? RoomAccessibilityIds { get; init; }
    }
}
