namespace TravelbilityApp.Core.DTOs.Room
{
    public class MainRoomDto
    {
        public Guid Id { get; init; }
        public string RoomTypeName { get; init; } = null!;
        public string MainBedTypeName { get; init; } = null!;
        public decimal PricePerNight { get; init; }
        public bool IsAccessibleRoom { get; init; }
    }
}
