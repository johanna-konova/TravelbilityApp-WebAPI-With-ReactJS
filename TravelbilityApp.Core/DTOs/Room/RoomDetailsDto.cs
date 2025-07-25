namespace TravelbilityApp.Core.DTOs.Room
{
    public class RoomDetailsDto : RoomShortDetailsDto
    {
        public int NumberOfUnits { get; init; }
        public string Description { get; init; } = null!;
        public IEnumerable<string> PhotoUrls { get; init; } = null!;
    }
}
