namespace TravelbilityApp.Core.DTOs.Property
{
    public class PropertyInAllDto : MainPropertyDto
    {
        public string MainPhoto { get; init; } = null!;
        public IEnumerable<string> Accessibility { get; init; } = null!;
    }
}
