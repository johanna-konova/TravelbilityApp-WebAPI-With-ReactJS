namespace TravelbilityApp.Core.DTOs.Property
{
    public class MainPropertyDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public int? StarsCount { get; init; }
        public string Address { get; init; } = null!;
        public string MainPhotoUrl { get; init; } = null!;
        public Guid PublisherId { get; init; }
    }
}
