using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelbilityApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameSomeColumnsInRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Rooms",
                newName: "SizeInSquareMeters");

            migrationBuilder.RenameColumn(
                name: "MaxGuestCapacity",
                table: "Rooms",
                newName: "MaxGuest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SizeInSquareMeters",
                table: "Rooms",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "MaxGuest",
                table: "Rooms",
                newName: "MaxGuestCapacity");
        }
    }
}
