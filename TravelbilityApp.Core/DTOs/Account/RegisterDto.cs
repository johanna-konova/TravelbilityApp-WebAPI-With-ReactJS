using System.ComponentModel.DataAnnotations;

using TravelbilityApp.Core.Attributes;

using static TravelbilityApp.Core.Constants.ModelsConstants;
using static TravelbilityApp.Core.Constants.ModelsMessagesConstants.Account;
using static TravelbilityApp.Core.Constants.ModelsMessagesConstants.Common;

namespace TravelbilityApp.Core.DTOs.Account
{
    public class RegisterDto
    {
        [Required(ErrorMessage = RequiredEmail)]
        [EmailAddress(ErrorMessage = InvalidEmailFormat)]
        public string Email { get; init; } = null!;

        [Required(ErrorMessage = RequiredPassword)]
        [StringLength(PasswordMaxLength,
            MinimumLength = PasswordMinLength,
            ErrorMessage = InvalidStringLength)]
        [StrongPassword]
        public string Password { get; init; } = null!;

        [Compare(nameof(Password), ErrorMessage = MismatchedPasswords)]
        public string ConfirmedPassword { get; init; } = string.Empty;
    }
}
