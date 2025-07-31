using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelbilityApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedApplicationUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-ab12-c3de4f567890"), 0, "7f3fbb67-83a1-4c8a-9376-77d5bc93e96e", "peter@abv.com", true, true, null, "PETER@ABV.COM", "PETER@ABV.COM", "AQAAAAIAAYagAAAAEAcs6V3mFpUPNWXrszVD+IVrtmbIkzeZTEiPewD6q5gcNVw2E2pfOnAhCB5/kfSXQA==", null, false, "XKYK5BIDWLG3ED57QZYQHRLUZMMUVYWS", false, "peter@abv.com" },
                    { new Guid("f7e8d9a0-b1c2-34d5-6789-f01ab2c345de"), 0, "5d4fdb87-23b1-4da2-9e3e-a8b4df7a283f", "george@abv.com", true, true, null, "GEORGE@ABV.COM", "GEORGE@ABV.COM", "AQAAAAIAAYagAAAAENb5p91Dl6o9bSI570GoO7UEalcQ/gGuB8TJecq9clPpkGKSM0j7jQ8OaH3kEH9mtw==", null, false, "JTHY3GADWFA4KD67TFYUIUQNLJMNXYAS", false, "george@abv.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-ab12-c3de4f567890"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7e8d9a0-b1c2-34d5-6789-f01ab2c345de"));
        }
    }
}
