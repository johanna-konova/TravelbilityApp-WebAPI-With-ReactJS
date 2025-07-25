namespace TravelbilityApp.Core.DTOs.Property
{
    public class PropertyInAllDto : MainPropertyDto
    {
        public IEnumerable<string> AccessibilityNames { get; init; } = null!;
    }
}
