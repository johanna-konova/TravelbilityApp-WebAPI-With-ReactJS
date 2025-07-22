namespace TravelbilityApp.Core.DTOs.Room
{
    public class RoomShortInfoDto
    {
        public Guid Id { get; init; }
        public string RoomTypeName { get; init; } = null!;
        public string MainBedTypeName { get; init; } = null!;
        public decimal PricePerNight { get; init; }
        public string MainPhotoUrl { get; init; } = null!;
        public bool IsAccessibleRoom { get; init; }
    }
}
