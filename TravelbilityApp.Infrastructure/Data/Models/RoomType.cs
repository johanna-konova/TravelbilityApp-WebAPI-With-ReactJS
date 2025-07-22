using System.ComponentModel.DataAnnotations;

namespace TravelbilityApp.Infrastructure.Data.Models
{
    public class RoomType
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Name { get; set; } = null!;

        public bool IsForAccessibility { get; set; }
    }
}
