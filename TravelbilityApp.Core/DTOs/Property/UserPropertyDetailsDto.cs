namespace TravelbilityApp.Core.DTOs.Property
{
    public class UserPropertyDetailsDto : PropertyDetailsDto
    {
        public string TypeName { get; init; } = null!;
        public string CheckIn { get; init; } = null!;
        public string CheckOut { get; init; } = null!;
    }
}
