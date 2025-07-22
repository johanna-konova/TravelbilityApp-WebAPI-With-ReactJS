using TravelbilityApp.Core.Services;
using TravelbilityApp.Infrastructure.Data.Models;

namespace TravelbilityApp.Core
{
    public static class CommonHelpers
    {
        public static IEnumerable<string> GetValidImageUrls(IEnumerable<string> imageUrls)
            => imageUrls
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Where(IsImageUrlValid);

        public static bool IsImageUrlValid(string imageUrl)
            => Uri.TryCreate(imageUrl, UriKind.Absolute, out var createdUrl) &&
                             (createdUrl.Scheme == Uri.UriSchemeHttp || createdUrl.Scheme == Uri.UriSchemeHttps);

        public static (IEnumerable<PropertyFacility> facilitiesToAdd, IEnumerable<PropertyFacility> facilitiesToRemove) GetFacilitiesToAddAndToRemoveAsync(
            IEnumerable<PropertyFacility> entityFacilities,
            IEnumerable<int> validSelectedFacilityIds,
            Guid propertyId,
            Guid? roomId = null)
        {
            var facilitiesToAdd = validSelectedFacilityIds
                .Except(entityFacilities.Select(f => f.FacilityId))
                .Select(vsfi => new PropertyFacility()
                {
                    FacilityId = vsfi,
                    PropertyId = propertyId,
                    RoomId = roomId
                })
                .ToList();

            var facilitiesToRemove = entityFacilities
                .Where(f => validSelectedFacilityIds.Contains(f.FacilityId) == false)
                .ToList();

            return (facilitiesToAdd, facilitiesToRemove);
        }

        public static (IEnumerable<PropertyPhoto> photosToAdd, IEnumerable<PropertyPhoto> photosToRemove) GetPhotosToAddAndToRemove(
            IEnumerable<PropertyPhoto> entityPhotos,
            IEnumerable<string> imageUrls,
            Guid propertyId,
            Guid? roomId = null)
        {
            var validImageUrls = GetValidImageUrls(imageUrls);

            var photosToRemove = entityPhotos
                .Where(p => validImageUrls.Contains(p.Url, StringComparer.OrdinalIgnoreCase) == false)
                .ToList();

            var photosToAdd = validImageUrls
                .Except(entityPhotos.Select(p => p.Url), StringComparer.OrdinalIgnoreCase)
                .Select(viu => new PropertyPhoto()
                {
                    Url = viu,
                    PropertyId = propertyId,
                    RoomId = roomId
                })
                .ToList();

            return (photosToAdd, photosToRemove);
        }
    }
}
