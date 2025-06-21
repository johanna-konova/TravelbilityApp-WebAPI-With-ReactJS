namespace TravelbilityApp.Core.DTOs.Property
{
    public class PropertyDetailsDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public PropertyTypeOptionDto Type { get; init; } = null!;
        public int? StarsCount { get; init; }
        public TimeOnly CheckIn { get; init; }
        public TimeOnly CheckOut { get; init; }
        public string Address { get; init; } = null!;
        public string Description { get; init; } = null!;
        public IEnumerable<PropertyFacilityOptionDto> Facilities { get; init; } = null!;
        public IEnumerable<string> ImageUrls { get; init; } = null!;
        public Guid PublisherId { get; init; }
    }
}