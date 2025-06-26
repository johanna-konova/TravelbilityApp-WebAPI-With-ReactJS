namespace TravelbilityApp.Core.DTOs.Property
{
    public class UserPropertyDto : MainPropertyDto
    {
        public string Description { get; init; } = null!;
        public string MainPhotoUrl { get; init; } = null!;
    }
}
