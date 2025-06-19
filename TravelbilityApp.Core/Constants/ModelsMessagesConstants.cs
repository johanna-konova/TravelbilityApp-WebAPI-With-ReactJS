namespace TravelbilityApp.Core.Constants
{
    public static class ModelsMessagesConstants
    {
        public const string InvalidStringLength = "The {0} must be between {2} and {1} characters long.";

        public static class Account
        {
            public const string RequiredEmail = "Please, enter your Email address.";
            public const string InvalidEmailFormat = "Please, enter a valid Email address.";
            public const string RequiredPassword = "Please, enter your Password.";
            public const string MismatchedPasswords = "The Password and Confirmation Password do not match.";
        }

    }
}
