using System.ComponentModel.DataAnnotations;

namespace TravelbilityApp.Core.DTOs.Account
{
    public class LogoutRequestDto
    {
        [Required]
        public string RefreshToken { get; init; } = null!;
    }
}
