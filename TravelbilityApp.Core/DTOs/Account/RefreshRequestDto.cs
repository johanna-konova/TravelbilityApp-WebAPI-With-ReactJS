using System.ComponentModel.DataAnnotations;

namespace TravelbilityApp.Core.DTOs.Account
{
    public class RefreshRequestDto : LogoutRequestDto
    {
        [Required]
        public Guid UserId { get; init; }
    }
}
