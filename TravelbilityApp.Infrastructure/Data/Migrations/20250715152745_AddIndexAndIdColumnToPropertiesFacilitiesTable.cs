using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelbilityApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexAndIdColumnToPropertiesFacilitiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertiesFacilities",
                table: "PropertiesFacilities");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PropertiesFacilities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertiesFacilities",
                table: "PropertiesFacilities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesFacilities_PropertyId_FacilityId_RoomId",
                table: "PropertiesFacilities",
                columns: new[] { "PropertyId", "FacilityId", "RoomId" },
                unique: true,
                filter: "[RoomId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertiesFacilities",
                table: "PropertiesFacilities");

            migrationBuilder.DropIndex(
                name: "IX_PropertiesFacilities_PropertyId_FacilityId_RoomId",
                table: "PropertiesFacilities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PropertiesFacilities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertiesFacilities",
                table: "PropertiesFacilities",
                columns: new[] { "PropertyId", "FacilityId" });
        }
    }
}
