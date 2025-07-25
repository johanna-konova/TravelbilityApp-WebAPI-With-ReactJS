using TravelbilityApp.Core.DTOs.Facility;

namespace TravelbilityApp.Core.DTOs.Property
{
    public class PropertyForEditDto : UserPropertyDto
    {
        public PropertyTypeOptionDto Type { get; init; } = null!;
        public TimeOnly CheckIn { get; init; }
        public TimeOnly CheckOut { get; init; }
        public IEnumerable<FacilityOptionDto> Facilities { get; init; } = null!;
        public IEnumerable<string> PhotoUrls { get; init; } = null!;
    }
}