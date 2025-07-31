using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelbilityApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedPropertiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Address", "CheckIn", "CheckOut", "CreatedAt", "Description", "Name", "PropertyTypeId", "PublisherId", "StarsCount", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"), "Boduthakurufaanu Magu, 20006 Male City, Maldives", new TimeOnly(14, 0, 0), new TimeOnly(12, 0, 0), new DateTime(2025, 7, 31, 12, 34, 9, 253, DateTimeKind.Utc).AddTicks(2156), "Beachfront Location: Summer Beach Maldives in Male City offers direct beachfront access with stunning sea views. Guests enjoy a terrace and outdoor seating area, perfect for relaxation.\r\n\r\nComfortable Accommodations: Rooms feature air-conditioning, private bathrooms, and modern amenities such as free WiFi, mini-bars, and flat-screen TVs. Family rooms and interconnected rooms cater to all travelers.\r\n\r\nDining Experience: The family-friendly restaurant serves Indian, Italian, Thai, and international cuisines, including vegetarian and halal options. Breakfast includes local specialties, fresh pastries, and a variety of beverages.\r\n\r\nConvenient Services: The guest house provides a free airport shuttle service, 24-hour front desk, concierge, and tour desk. Additional amenities include a coffee shop, child-friendly buffet, and free WiFi throughout the property.", "Summer Beach Maldives", 5, new Guid("f7e8d9a0-b1c2-34d5-6789-f01ab2c345de"), 4, 3, new DateTime(2025, 7, 31, 12, 34, 9, 253, DateTimeKind.Utc).AddTicks(2157) },
                    { new Guid("bae99276-1865-4c63-899c-093d3b85f014"), "Palm Jumeirah, Palm Jumeirah, Dubai, United Arab Emirates", new TimeOnly(15, 0, 0), new TimeOnly(6, 0, 0), new DateTime(2025, 7, 31, 12, 34, 9, 253, DateTimeKind.Utc).AddTicks(1931), "FIVE Palm Jumeirah Dubai features its own private beach as well as 5 outdoor swimming pools, including a 180 ft long option, running through the heart of the resort. Guests can enjoy free WiFi throughout the property.\r\n\r\nThe hotel has 470 guest rooms and suites, spread across 16 floors, decorated in a simple yet elegant style with views of the Arabian Gulf.\r\n\r\nThe resort has an array of facilities, including dining venues hosted by world-class chefs, a modern spa and a karaoke room at Maiden Shanghai.\r\n\r\nA landmark on the trunk of the iconic Palm Jumeirah, FIVE Palm Jumeirah Dubai is strategically located for convenient access to Dubai’s business districts, as well as the city’s many exciting tourist and entertainment attractions.\r\n\r\nThe resort is also accessible from Dubai International Airport (DXB), which is 20 mi away and Al Maktoum International Airport (DWC), which 26 mi away. Mall of Emirates is 7 mi away, while Dubai Mall is 14 mi from the property.", "FIVE Palm Jumeirah Dubai", 3, new Guid("a1b2c3d4-e5f6-7890-ab12-c3de4f567890"), 5, 3, new DateTime(2025, 7, 31, 12, 34, 9, 253, DateTimeKind.Utc).AddTicks(1935) },
                    { new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), "Barbaros Hayrettin Pasa Mah. 1999 Sok. Esenyurt, 34522 Istanbul, Turkey", new TimeOnly(14, 0, 0), new TimeOnly(0, 0, 0), new DateTime(2025, 7, 31, 12, 34, 9, 253, DateTimeKind.Utc).AddTicks(2143), "Located in business area in Beylikduzu, 1 km from Tuyap Convention Centre, Hilton Garden Inn Istanbul Beylikduzu features indoor pool and 24/7 fitness centre. Free WiFi access is available in all areas.\n\nModern rooms are fitted with a flat-screen TV. Some units include a seating area for your convenience. Every room comes with a private bathroom. For your comfort, you will find free toiletries and a hair dryer.\n\nThere is a 24-hour front desk, providing room service at the property. Laundry, dry cleaning and ironing services are also provided upon request at an additional charge.\n\nGuests can enjoy their meals at the on-site restaurant. The lobby bar is ideal for having a drink and relaxing after a busy day.\n\nThe hotel is 35 km from Istanbul’s historic centre, where guests can visit Topkapi Palace, Blue Mosque, and Hagia Sophia Museum. Istanbul Airport is a 50-minute drive away.", "Hilton Garden Inn Istanbul Beylikduzu", 1, new Guid("a1b2c3d4-e5f6-7890-ab12-c3de4f567890"), 4, 2, new DateTime(2025, 7, 31, 12, 34, 9, 253, DateTimeKind.Utc).AddTicks(2144) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"));

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("bae99276-1865-4c63-899c-093d3b85f014"));

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-ab12-c3de4f567890"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAcs6V3mFpUPNWXrszVD+IVrtmbIkzeZTEiPewD6q5gcNVw2E2pfOnAhCB5/kfSXQA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7e8d9a0-b1c2-34d5-6789-f01ab2c345de"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENb5p91Dl6o9bSI570GoO7UEalcQ/gGuB8TJecq9clPpkGKSM0j7jQ8OaH3kEH9mtw==");
        }
    }
}
