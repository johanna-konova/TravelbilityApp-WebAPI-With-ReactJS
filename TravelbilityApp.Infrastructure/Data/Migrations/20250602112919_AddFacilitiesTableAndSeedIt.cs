using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelbilityApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFacilitiesTableAndSeedIt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsForAccessibility = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "IsForAccessibility", "Name" },
                values: new object[,]
                {
                    { 1, true, "Higher level toilet" },
                    { 2, false, "Airport shuttle" },
                    { 3, false, "Free WiFi" },
                    { 4, false, "Pets allowed" },
                    { 5, true, "Toilet with grab rails" },
                    { 6, false, "Room service" },
                    { 7, true, "Wheelchair accessible" },
                    { 8, true, "Visual aids: Tactile signs" },
                    { 9, false, "Swimming Pool" },
                    { 10, true, "Electric vehicle charging station" },
                    { 11, false, "Family rooms" },
                    { 12, true, "Visual aids: Braille" },
                    { 13, true, "Lower bathroom sink" },
                    { 14, false, "Fitness centre" },
                    { 15, false, "Non-smoking rooms" },
                    { 16, true, "Emergency cord in bathroom" },
                    { 17, false, "Parking" },
                    { 18, false, "Restaurant" },
                    { 19, true, "Auditory guidance" },
                    { 20, false, "Spa and wellness centre" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facilities");
        }
    }
}
