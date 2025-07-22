namespace TravelbilityApp.Core.Constants
{
    public static class ModelsMessagesConstants
    {
        public static class Common
        {
            public const string InvalidStringLength = "The {0} must be between {2} and {1} characters long.";

            public const string RequiredType = "Please, select a Type.";
            public const string RequiredDescription = "Please, enter a Description.";
            public const string RequiredAtLeastFacility = "Please, select at least 1 Facility.";
            public const string RequiredAtLeastAccessibility = "Please, select at least 1 Accessibility.";
            public const string RequiredImageUrlFormat = "Invalid Photo URL format.";
            public const string DuplicateImageUrlFormat = "Duplicate Photo URL.";
            public const string InvalidImageUrlsCount = "Please, upload at least {0} more Photo/s with a valid URL format/s.";
        }

        public static class Account
        {
            public const string RequiredEmail = "Please, enter your Email address.";
            public const string InvalidEmailFormat = "Please, enter a valid Email address.";
            public const string RequiredPassword = "Please, enter your Password.";
            public const string MismatchedPasswords = "The Password and Confirmation Password do not match.";
        }

        public static class Property
        {
            public const string RequiredName = "Please, enter a Name.";
            public const string InvalidStarsCount = "The Stars must be between {1} and {2} count.";
            public const string RequiredCheckInAndCheckOutFormat = "Please, enter a valid time in HH:mm format.";
            public const string RequiredAddress = "Please, enter an Address.";
            public const string RequiredAtLeastAccessibleRoom = "Please, add at least one Accessible Room.";
        }

        public static class Room
        {
            public const string RequiredMaxGuestCapacity = "Please, select Maximum count of guests.";
            public const string InvalidMaxGuestCapacity = "The Maximum count of guests must be between {1} and {2}.";
            public const string RequiredPricePerNight = "Please, enter a valid Price per night.";
            public const string InvalidPricePerNight = "The Price per night must be at least {1} EUR.";
            public const string RequiredMainBedType = "Please, select a Main bed type.";
            public const string RequiredSize = "Please, enter a valid Room size.";
            public const string InvalidSize = "The Room size must be between {1} and {2} m².";
            public const string RequiredNumberOfUnits = "Please, enter a valid Number of Rooms.";
            public const string InvalidNumberOfUnits = "The Number of Rooms must be between {1} and {2}.";
        }
    }
}
