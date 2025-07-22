using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static TravelbilityApp.Infrastructure.Data.Constants.DataConstants.Room;

namespace TravelbilityApp.Infrastructure.Data.Models
{
    public class Room
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();

        public int RoomTypeId { get; set; }

        public int MaxGuests { get; set; }

        [Precision(18, 2)]
        public decimal PricePerNight { get; set; }

        public int MainBedTypeId { get; set; }

        public double SizeInSquareMeters { get; set; }

        public int NumberOfUnits { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey(nameof(RoomTypeId))]
        public RoomType RoomType { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(MainBedTypeId))]
        public BedType MainBedType { get; set; } = null!;

        public Guid PropertyId { get; set; }

        [Required]
        [ForeignKey(nameof(PropertyId))]
        public Property Property { get; set; } = null!;

        public IEnumerable<PropertyFacility> Facilities { get; set; } = null!;

        public IEnumerable<PropertyPhoto> Photos { get; set; } = null!;
    }
}
