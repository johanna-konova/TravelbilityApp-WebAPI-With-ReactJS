using System.ComponentModel.DataAnnotations;

namespace TravelbilityApp.Core.Attributes
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value as string ?? string.Empty;
            var errorMessages = new List<string>();

            if (password.Any(char.IsDigit) == false)
            {
                errorMessages.Add("one digit");
            }

            if (password.Any(char.IsLower) == false)
            {
                errorMessages.Add("one lowercase letter");
            }

            if (password.Any(char.IsUpper) == false)
            {
                errorMessages.Add("one uppercase letter");
            }

            if (password.Any(ch => char.IsLetterOrDigit(ch) == false) == false)
            {
                errorMessages.Add("one special character");
            }

            if (errorMessages.Count != 0)
            {
                string joinedErrorMessages = errorMessages.Count > 1
                    ? string.Join(", ", errorMessages.Take(errorMessages.Count - 1)) + " and " + errorMessages.Last()
                    : errorMessages.First();

                return new ValidationResult($"The password must contain at least {joinedErrorMessages}.");
            }

            return ValidationResult.Success;
        }
    }

}
