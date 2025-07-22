using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TravelbilityApp.Infrastructure.Data.Models.Enums;

using static TravelbilityApp.Infrastructure.Data.Constants.DataConstants.Property;

namespace TravelbilityApp.Infrastructure.Data.Models
{
    public class Property
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public int PropertyTypeId { get; set; }

        public int? StarsCount { get; set; }

        public TimeOnly CheckIn { get; set; }

        public TimeOnly CheckOut { get; set; }

        [Required]
        [StringLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public PropertyStatus Status { get; set; } = PropertyStatus.Saved;

        [Required]
        [ForeignKey(nameof(PropertyTypeId))]
        public PropertyType PropertyType { get; set; } = null!;

        public Guid PublisherId { get; set; }

        [Required]
        [ForeignKey(nameof(PublisherId))]
        public ApplicationUser Publisher { get; set; } = null!;

        public IEnumerable<PropertyFacility> Facilities { get; set; } = null!;

        public IEnumerable<PropertyPhoto> Photos { get; set; } = null!;

        public IEnumerable<Room> Rooms { get; set; } = null!;
    }
}