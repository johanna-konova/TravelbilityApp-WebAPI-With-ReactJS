namespace TravelbilityApp.Infrastructure.Data.Constants
{
    public static class DataConstants
    {
        public static class Property
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;

            public const int StarsCountMinValue = 1;
            public const int StarsCountMaxValue = 5;

            public const int AddressMinLength = 10;
            public const int AddressMaxLength = 200;

            public const int DescriptionMinLength = 20;
            public const int DescriptionMaxLength = 1000;
        }

        public static class Facility
        {
            public const int WhereStatusAsStringMaxLength = 20;
        }

        public static class PropertyPhoto
        {
            public const int UrlMaxLength = 500;
        }

        public static class Room
        {
            public const int MaxGuestCapacityMinValue = 1;
            public const int MaxGuestCapacityMaxValue = 6;

            public const string PricePerNightMinValueAsString = "0.01";
            public const string PricePerNightMaxValueAsString = "79228162514264337593543950335.00";

            public const double SizeMinValue = 5.00;
            public const double SizeMaxValue = 5000.00;

            public const int NumberOfUnitsMinValue = 1;
            public const int NumberOfUnitsMaxValue = 8000;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 1000;
        }
    }
}
