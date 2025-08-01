namespace TravelbilityApp.Core.DTOs.Account
{
    public class ResponseDto
    {
        public Guid Id { get; init; }
        public string Email { get; init; } = null!;
        public string AccessToken { get; init; } = null!;
        public DateTime Expires { get; init; }
        public string RefreshToken { get; init; } = null!;
        public bool IsAdmin { get; init; }
    }
}
