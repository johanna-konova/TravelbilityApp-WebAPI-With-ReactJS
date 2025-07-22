using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelbilityApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "PropertiesPhotos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "PropertiesFacilities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MaxGuestCapacity = table.Column<int>(type: "int", nullable: false),
                    MainBedTypeId = table.Column<int>(type: "int", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false),
                    NumberOfUnits = table.Column<int>(type: "int", nullable: false),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_BedTypes_MainBedTypeId",
                        column: x => x.MainBedTypeId,
                        principalTable: "BedTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesPhotos_RoomId",
                table: "PropertiesPhotos",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesFacilities_RoomId",
                table: "PropertiesFacilities",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_MainBedTypeId",
                table: "Rooms",
                column: "MainBedTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PropertyId",
                table: "Rooms",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertiesFacilities_Rooms_RoomId",
                table: "PropertiesFacilities",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertiesPhotos_Rooms_RoomId",
                table: "PropertiesPhotos",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertiesFacilities_Rooms_RoomId",
                table: "PropertiesFacilities");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertiesPhotos_Rooms_RoomId",
                table: "PropertiesPhotos");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_PropertiesPhotos_RoomId",
                table: "PropertiesPhotos");

            migrationBuilder.DropIndex(
                name: "IX_PropertiesFacilities_RoomId",
                table: "PropertiesFacilities");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "PropertiesPhotos");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "PropertiesFacilities");
        }
    }
}
