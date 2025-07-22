using FluentValidation;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.DTOs.Room;
using TravelbilityApp.Infrastructure.Data.Models.Enums;

using static TravelbilityApp.Core.Constants.ModelsConstants;
using static TravelbilityApp.Core.Constants.ModelsMessagesConstants.Common;
using static TravelbilityApp.Core.Constants.ModelsMessagesConstants.Room;
using static TravelbilityApp.Core.CommonHelpers;

namespace TravelbilityApp.WebAPI.Validators
{
    public class RoomInputDtoValidator : AbstractValidator<RoomInputDto>
    {
        public RoomInputDtoValidator(
            IRoomTypeService roomTypeService,
            IBedTypeService bedTypeService,
            IFacilityService facilityService)
        {
            RuleFor(cpd => cpd.RoomTypeId)
                .MustAsync(async (typeId, cancellationToken) =>
                    await roomTypeService.HasRoomTypeWithGivenIdAsync(typeId!.Value))
                .When(cpd => cpd.RoomTypeId.HasValue)
                .WithMessage(RequiredType);

            RuleFor(cpd => cpd.MainBedTypeId)
                .MustAsync(async (typeId, cancellationToken) =>
                    await bedTypeService.HasBedTypeWithGivenIdAsync(typeId!.Value))
                .When(cpd => cpd.MainBedTypeId.HasValue)
                .WithMessage(RequiredMainBedType);

            RuleFor(cpd => cpd.FacilityIds)
                .CustomAsync(async (facilityIds, validationContext, cancellationToken) =>
                {
                    var validSelectedFacilities = await facilityService.GetValidSelectedAsync(facilityIds, WhereStatus.OnlyInRoom);

                    if (validSelectedFacilities.Any(vsf => vsf.IsForAccessibility == false) == false)
                    {
                        validationContext.AddFailure("CommonFacilityIds", RequiredAtLeastFacility);
                    }

                    var isRoomAccessible = await roomTypeService.IsRoomAccessibleAsync(validationContext.InstanceToValidate.RoomTypeId);

                    if (isRoomAccessible == true &&
                        validSelectedFacilities.Any(vsf => vsf.IsForAccessibility) == false)
                    {
                        validationContext.AddFailure("AccessibilityIds", RequiredAtLeastAccessibility);
                    }
                });

            RuleFor(cpd => cpd.PhotoUrls)
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
