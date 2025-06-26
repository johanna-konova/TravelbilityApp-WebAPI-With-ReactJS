namespace TravelbilityApp.Core.DTOs.Property
{
    public class PropertyDetailsDto : MainPropertyDto
    {
        public PropertyTypeOptionDto Type { get; init; } = null!;
        public TimeOnly CheckIn { get; init; }
        public TimeOnly CheckOut { get; init; }
        public string Description { get; init; } = null!;
        public IEnumerable<PropertyFacilityOptionDto> Facilities { get; init; } = null!;
        public IEnumerable<string> PhotoUrls { get; init; } = null!;
    }
}