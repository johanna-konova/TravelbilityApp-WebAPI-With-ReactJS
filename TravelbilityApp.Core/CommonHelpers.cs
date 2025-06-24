namespace TravelbilityApp.Core
{
    public class CommonHelpers
    {
        public static IEnumerable<string> GetValidImageUrls(IEnumerable<string> imageUrls)
            => imageUrls
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Where(IsImageUrlValid);

        public static bool IsImageUrlValid(string imageUrl)
            => Uri.TryCreate(imageUrl, UriKind.Absolute, out var createdUrl) &&
                             (createdUrl.Scheme == Uri.UriSchemeHttp || createdUrl.Scheme == Uri.UriSchemeHttps);
    }
}
