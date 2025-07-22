using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelbilityApp.Infrastructure.Data.Models
{
    [Index(nameof(PropertyId), nameof(FacilityId), nameof(RoomId), IsUnique = true)]
    public class PropertyFacility
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid PropertyId { get; init; }

        [ForeignKey(nameof(PropertyId))]
        public Property Property { get; init; } = null!;

        public Guid? RoomId { get; init; }

        [ForeignKey(nameof(RoomId))]
        public Room? Room { get; init; }

        public int FacilityId { get; init; }

        [ForeignKey(nameof(FacilityId))]
        public Facility Facility { get; init; } = null!;
    }
}