using System.ComponentModel.DataAnnotations;

using static TravelbilityApp.Core.Constants.ModelsMessagesConstants;
using static TravelbilityApp.Core.Constants.ModelsMessagesConstants.Property;
using static TravelbilityApp.Infrastructure.Data.Constants.DataConstants.Property;

namespace TravelbilityApp.Core.DTOs.Property
{
    public class PropertyInputDto
    {
        [Required(ErrorMessage = RequiredName)]
        [StringLength(NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = InvalidStringLength)]
        public string Name { get; set; } = null!;

        [Range(StarsCountMinValue, StarsCountMaxValue,
            ErrorMessage = InvalidStarsCount)]
        public int? StarsCount { get; set; }

        [Required(ErrorMessage = RequiredCheckInAndCheckOutFormat)]
        public TimeOnly? CheckIn { get; set; }

        [Required(ErrorMessage = RequiredCheckInAndCheckOutFormat)]
        public TimeOnly? CheckOut { get; set; }

        [Required(ErrorMessage = RequiredAddress)]
        [StringLength(AddressMaxLength,
            MinimumLength = AddressMinLength,
            ErrorMessage = InvalidStringLength)]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = RequiredDescription)]
        [StringLength(DescriptionMaxLength,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = InvalidStringLength)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = RequiredType)]
        public int? TypeId { get; set; }

        public IEnumerable<int?> FacilityIds { get; set; } = new HashSet<int?>();

        public IList<string> ImageUrls { get; set; } = new List<string>();
    }
}