namespace TravelbilityApp.Core.DTOs.Property
{
    public class PropertyQueryParamsDto
    {
        public IEnumerable<int>? PropertyTypeIds { get; init; }
        public IEnumerable<int>? FacilityIds { get; init; }
        public IEnumerable<int>? AccessibilityIds { get; init; }
    }
}
