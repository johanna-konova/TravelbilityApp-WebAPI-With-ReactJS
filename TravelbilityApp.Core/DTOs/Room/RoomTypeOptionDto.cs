namespace TravelbilityApp.Core.DTOs.Room
{
    public class RoomTypeOptionDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public bool IsForAccessibility { get; set; }
    }
}