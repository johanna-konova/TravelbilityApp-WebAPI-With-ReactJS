namespace TravelbilityApp.Core.DTOs.Property
{
    public class PropertyDetailsDto : MainPropertyDto
    {
        public string Description { get; init; } = null!;
        public IEnumerable<string> CommonFacilityNames { get; init; } = null!;
        public IEnumerable<string> AccessibilityNames { get; init; } = null!;
        public IEnumerable<string> PhotoUrls { get; init; } = null!;
    }
}
