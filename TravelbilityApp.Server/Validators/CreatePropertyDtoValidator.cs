using FluentValidation;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Property;

using static TravelbilityApp.Core.Constants.ModelsConstants;
using static TravelbilityApp.Core.Constants.ModelsMessagesConstants.Property;
using static TravelbilityApp.Core.CommonHelpers;

namespace TravelbilityApp.WebAPI.Validators
{
    public class CreatePropertyDtoValidator : AbstractValidator<PropertyInputDto>
    {
        public CreatePropertyDtoValidator(
            IPropertyTypeService propertyTypeService,
            IFacilityService facilityService)
        {
            RuleFor(cpd => cpd.TypeId)
                .MustAsync(async (typeId, cancellationToken) =>
                    await propertyTypeService.HasPropertyTypeWithGivenIdAsync(typeId!.Value))
                .When(cpd => cpd.TypeId.HasValue)
                .WithMessage(RequiredType);

            RuleFor(cpd => cpd.FacilityIds)
                .CustomAsync(async (facilityIds, validationContext, cancellationToken) =>
                {
                    var validSelectedFacilities = await facilityService.GetValidSelectedAsync(facilityIds);

                    if (validSelectedFacilities.Any(vsf => vsf.IsForAccessibility == false) == false)
                    {
                        validationContext.AddFailure("CommonFacilityIds", RequiredAtLeastFacility);
                    }

                    if (validSelectedFacilities.Any(vsf => vsf.IsForAccessibility) == false)
                    {
                        validationContext.AddFailure("AccessibilityIds", RequiredAtLeastAccessibility);
                    }
                });

            RuleFor(cpd => cpd.ImageUrls)
                .Custom((imageUrls, validationContext) =>
                {
                    var validImageUrls = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                for (int i = 0; i < imageUrls.Count; i++)
                {
                    var url = imageUrls[i];
                    var key = $"ImageUrls[{i}]";

                    if (IsImageUrlValid(url) == false)
                        {
                            validationContext.AddFailure(key, RequiredImageUrlFormat);
                            continue;
                        }

                        if (validImageUrls.Add(url) == false)
                        {
                            validationContext.AddFailure(key, DuplicateImageUrlFormat);
                            continue;
                        }
                    }

                    var validImageUrlsCount = validImageUrls.Count;

                    if (validImageUrlsCount < ValidImageUrlsMinCount)
                    {
                        validationContext.AddFailure("ImageUrlsCount", string.Format(InvalidImageUrlsCount, ValidImageUrlsMinCount - validImageUrlsCount));
                    }
                });
        }
    }
}
