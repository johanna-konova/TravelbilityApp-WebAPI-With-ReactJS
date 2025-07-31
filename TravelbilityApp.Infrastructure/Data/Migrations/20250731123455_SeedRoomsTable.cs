using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelbilityApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoomsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-ab12-c3de4f567890"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKVx91uWOkjZzGCxXb1RJuKJDHVqLCuPN8WlMefopl3z9zCdkbHsD3KbsetZAXzDLg==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7e8d9a0-b1c2-34d5-6789-f01ab2c345de"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBsn1zTpRKY2zsgaVQ0BhEpmM0j2H4sv6+HqFlfeKmJgF8++XVw2FZwAkJQBWL2Yqw==");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 12, 34, 54, 470, DateTimeKind.Utc).AddTicks(7177), new DateTime(2025, 7, 31, 12, 34, 54, 470, DateTimeKind.Utc).AddTicks(7178) });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("bae99276-1865-4c63-899c-093d3b85f014"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 12, 34, 54, 470, DateTimeKind.Utc).AddTicks(6787), new DateTime(2025, 7, 31, 12, 34, 54, 470, DateTimeKind.Utc).AddTicks(6895) });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 12, 34, 54, 470, DateTimeKind.Utc).AddTicks(7159), new DateTime(2025, 7, 31, 12, 34, 54, 470, DateTimeKind.Utc).AddTicks(7160) });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Description", "IsDeleted", "MainBedTypeId", "MaxGuests", "NumberOfUnits", "PricePerNight", "PropertyId", "RoomTypeId", "SizeInSquareMeters" },
                values: new object[,]
                {
                    { new Guid("135ea1ed-239f-437b-8ec7-9035dbb7f5b3"), "Featuring a private balcony and seating, this air conditioned room offers a large double bed, a 55 inch smart TV.", false, 3, 4, 3, 129.99m, new Guid("bae99276-1865-4c63-899c-093d3b85f014"), 8, 25.0 },
                    { new Guid("141e8179-d711-4dec-8ec6-7ec29d47b2b2"), "The air-conditioned suite features 2 bedrooms and 2 bathrooms with a bath and a shower. Featuring a balcony with sea views, this suite also provides a mini-bar and a flat-screen TV with cable channels. The unit offers 2 beds.", false, 3, 3, 5, 197m, new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"), 8, 43.0 },
                    { new Guid("534ed86f-6b26-4b46-8070-5be3770199f9"), "The double room offers air conditioning, a tea and coffee maker, a safe deposit box, heating and a flat-screen TV. The unit offers 2 beds.", false, 2, 2, 7, 109.98m, new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), 3, 39.0 },
                    { new Guid("e961fee4-03ab-4d12-a1cb-d25849b9a1c8"), "This spacious suite is consisted of of 1 bedroom, a seating area and 1 bathroom with a walk-in shower and a bath. The air-conditioned suite offers a flat-screen TV with streaming services, soundproof walls, a mini-bar, a tea and coffee maker as well as city views. The unit offers 1 bed.", false, 5, 2, 11, 357m, new Guid("bae99276-1865-4c63-899c-093d3b85f014"), 6, 55.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("135ea1ed-239f-437b-8ec7-9035dbb7f5b3"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("141e8179-d711-4dec-8ec6-7ec29d47b2b2"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("534ed86f-6b26-4b46-8070-5be3770199f9"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("e961fee4-03ab-4d12-a1cb-d25849b9a1c8"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-ab12-c3de4f567890"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEDgln4MVU6JCKS9p83eq6rZyP1AOtVHMz1TMMiwW33eqI7Omf2XbMNlE2e619Qelw==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7e8d9a0-b1c2-34d5-6789-f01ab2c345de"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOv7ni25W2Z1/qL34mQmpd54ptK/dhBAznmN7bjHSYWgLIDb0ipqwBdlHCroh+aO/Q==");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 12, 34, 9, 253, DateTimeKind.Utc).AddTicks(2156), new DateTime(2025, 7, 31, 12, 34, 9, 253, DateTimeKind.Utc).AddTicks(2157) });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("bae99276-1865-4c63-899c-093d3b85f014"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 12, 34, 9, 253, DateTimeKind.Utc).AddTicks(1931), new DateTime(2025, 7, 31, 12, 34, 9, 253, DateTimeKind.Utc).AddTicks(1935) });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 12, 34, 9, 253, DateTimeKind.Utc).AddTicks(2143), new DateTime(2025, 7, 31, 12, 34, 9, 253, DateTimeKind.Utc).AddTicks(2144) });
        }
    }
}
