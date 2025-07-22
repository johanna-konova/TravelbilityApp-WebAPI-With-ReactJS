using System.ComponentModel.DataAnnotations;

using static TravelbilityApp.Core.Constants.ModelsMessagesConstants.Common;
using static TravelbilityApp.Core.Constants.ModelsMessagesConstants.Room;
using static TravelbilityApp.Infrastructure.Data.Constants.DataConstants.Room;

namespace TravelbilityApp.Core.DTOs.Room
{
    public class RoomInputDto
    {
        [Required(ErrorMessage = RequiredType)]
        public int? RoomTypeId { get; set; }

        [Required(ErrorMessage = RequiredMaxGuestCapacity)]
        [Range(MaxGuestCapacityMinValue, MaxGuestCapacityMaxValue,
            ErrorMessage = InvalidMaxGuestCapacity)]
        public int? MaxGuests { get; set; }

        [Required(ErrorMessage = RequiredPricePerNight)]
        [Range(typeof(decimal),
            PricePerNightMinValueAsString, PricePerNightMaxValueAsString,
            ErrorMessage = InvalidPricePerNight)]
        public decimal? PricePerNight { get; set; }

        [Required(ErrorMessage = RequiredMainBedType)]
        public int? MainBedTypeId { get; set; }

        [Required(ErrorMessage = RequiredSize)]
        [Range(SizeMinValue, SizeMaxValue, ErrorMessage = InvalidSize)]
        public double? Size { get; set; }

        [Required(ErrorMessage = RequiredNumberOfUnits)]
        [Range(NumberOfUnitsMinValue, NumberOfUnitsMaxValue,
            ErrorMessage = InvalidNumberOfUnits)]
        public int? NumberOfUnits { get; set; }

        [Required(ErrorMessage = RequiredDescription)]
        [StringLength(DescriptionMaxLength,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = InvalidStringLength)]
        public string Description { get; set; } = null!;

        public Guid PropertyId { get; set; }

        public IEnumerable<int?> FacilityIds { get; set; } = new HashSet<int?>();

        public IList<string> PhotoUrls { get; set; } = new List<string>();
    }
}
