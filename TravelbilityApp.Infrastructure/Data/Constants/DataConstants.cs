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

        public static class PropertyPhoto
        {
            public const int UrlMaxLength = 500;
        }
    }
}
