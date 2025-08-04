namespace TravelbilityApp.Core.DTOs.Property
{
    public class PropertForAdminShortDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public string TypeName { get; init; } = null!;
        public string Address { get; init; } = null!;
        public string Publisher { get; init; } = null!;
        public string Status { get; init; } = null!;
    }
}
