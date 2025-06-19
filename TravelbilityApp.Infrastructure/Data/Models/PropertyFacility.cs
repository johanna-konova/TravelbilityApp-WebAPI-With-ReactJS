using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelbilityApp.Infrastructure.Data.Models
{
    [PrimaryKey(nameof(PropertyId), nameof(FacilityId))]
    public class PropertyFacility
    {
        public Guid PropertyId { get; init; }

        [ForeignKey(nameof(PropertyId))]
        public Property Property { get; init; } = null!;

        public int FacilityId { get; init; }

        [ForeignKey(nameof(FacilityId))]
        public Facility Facility { get; init; } = null!;
    }
}