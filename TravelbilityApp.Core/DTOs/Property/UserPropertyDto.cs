namespace TravelbilityApp.Core.DTOs.Property
{
    public class UserPropertyDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public int? StarsCount { get; init; }
        public string Address { get; init; } = null!;
        public string Description { get; init; } = null!;
        public string MainPhoto { get; init; } = null!;
    }
}
