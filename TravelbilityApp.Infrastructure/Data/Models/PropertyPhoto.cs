using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static TravelbilityApp.Infrastructure.Data.Constants.DataConstants.PropertyPhoto;

namespace TravelbilityApp.Infrastructure.Data.Models
{
    public class PropertyPhoto
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required]
        [StringLength(UrlMaxLength)]
        public string Url { get; set; } = null!;

        [Required]
        public Guid PropertyId { get; init; }

        [ForeignKey(nameof(PropertyId))]
        public Property Property { get; init; } = null!;
    }
}
