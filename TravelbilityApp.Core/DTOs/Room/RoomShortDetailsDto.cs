namespace TravelbilityApp.Core.DTOs.Room
{
    public class RoomShortDetailsDto : MainRoomDto
    {
        public int MaxGuests { get; init; }
        public double Size { get; init; }
        public IEnumerable<string> CommonFacilityNames { get; init; } = null!;
        public IEnumerable<string> AccessibilityNames { get; init; } = null!;
    }
}
