using TravelbilityApp.Core.DTOs.Facility;

namespace TravelbilityApp.Core.DTOs.Room
{
    public class RoomDetailsDto
    {
        public Guid Id { get; init; }
        public RoomTypeOptionDto RoomType { get; init; } = null!;
        public int MaxGuests { get; init; }
        public decimal PricePerNight { get; init; }
        public RoomBedTypeOptionDto MainBedType { get; init; } = null!;
        public double Size { get; init; }
        public int NumberOfUnits { get; init; }
        public string Description { get; init; } = null!;
        public IEnumerable<FacilityOptionDto> Facilities { get; init; } = null!;
        public IEnumerable<string> PhotoUrls { get; init; } = null!;
    }
}
