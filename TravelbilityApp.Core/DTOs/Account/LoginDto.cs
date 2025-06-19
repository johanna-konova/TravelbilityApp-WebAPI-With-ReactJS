using System.ComponentModel.DataAnnotations;

using static TravelbilityApp.Core.Constants.ModelsMessagesConstants.Account;

namespace TravelbilityApp.Core.DTOs.Account
{
    public class LoginDto
    {
        [Required(ErrorMessage = RequiredEmail)]
        public string Email { get; init; } = string.Empty;

        [Required(ErrorMessage = RequiredPassword)]
        public string Password { get; init; } = string.Empty;

        //public bool RememberMe { get; init; }
    }
}
