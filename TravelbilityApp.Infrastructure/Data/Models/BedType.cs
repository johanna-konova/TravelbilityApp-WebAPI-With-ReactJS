using System.ComponentModel.DataAnnotations;

namespace TravelbilityApp.Infrastructure.Data.Models
{
    public class BedType
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;
    }
}
