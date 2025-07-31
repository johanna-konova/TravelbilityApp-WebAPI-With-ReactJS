using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelbilityApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedPropertiesPhotosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-ab12-c3de4f567890"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJWqUtNTFyrCflRJ5mDSJBQ/+6zWDN0EOs71CJjYR+CdSVrC5EmSgljJfheS0OdepA==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7e8d9a0-b1c2-34d5-6789-f01ab2c345de"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKmgDhog2RCm4iw5ZcCQKxQMzwb43ifsn+Wiz+Dbfuqirl8NkgDWsqQxxDZz6QokGA==");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 13, 58, 31, 879, DateTimeKind.Utc).AddTicks(228), new DateTime(2025, 7, 31, 13, 58, 31, 879, DateTimeKind.Utc).AddTicks(228) });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("bae99276-1865-4c63-899c-093d3b85f014"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 13, 58, 31, 878, DateTimeKind.Utc).AddTicks(9874), new DateTime(2025, 7, 31, 13, 58, 31, 878, DateTimeKind.Utc).AddTicks(9879) });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 13, 58, 31, 879, DateTimeKind.Utc).AddTicks(204), new DateTime(2025, 7, 31, 13, 58, 31, 879, DateTimeKind.Utc).AddTicks(205) });

            migrationBuilder.InsertData(
                table: "PropertiesPhotos",
                columns: new[] { "Id", "PropertyId", "RoomId", "Url" },
                values: new object[,]
                {
                    { new Guid("0057040d-0289-4973-967c-d8f39f4c890f"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), new Guid("e961fee4-03ab-4d12-a1cb-d25849b9a1c8"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568934.jpg?k=af600b8487d4f95512b7af7c3de4a42a5dd340445bb98c5f3ade0a834af8ec61&o=" },
                    { new Guid("0a4feeda-1d8f-4fa5-9ede-2b7915333691"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), new Guid("135ea1ed-239f-437b-8ec7-9035dbb7f5b3"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568129.jpg?k=6231e8c3ce340683d6a5ea3a4c31319573f6fcb70e768fc8daac4dd4c30371b2&o=" },
                    { new Guid("14e34147-cb15-4b1c-98b8-f8e54e360f90"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), null, "https://c.ekstatic.net/dex-media/416/Five-Palm-Jumeirah-Dubai-Desktop-HotelDetails-1-4-637327495922065801.jpg" },
                    { new Guid("17221edc-72c8-49c4-ae5b-16a81c33a16d"), new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"), new Guid("141e8179-d711-4dec-8ec6-7ec29d47b2b2"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398788797.jpg?k=e78b542c382a6c006bb6393db978c8dd1b5e9279e71c65cd6abd02b069bba9c4&o=" },
                    { new Guid("199009c0-8e75-4d01-83cd-a6a57aeaa74a"), new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"), new Guid("141e8179-d711-4dec-8ec6-7ec29d47b2b2"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398788806.jpg?k=59ada28a447085e1328c22f48683ae09006e132f368ed5042157dd29b1c5576a&o=" },
                    { new Guid("19f9f6ad-57e9-4542-ae0f-59b257b1c0c0"), new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), null, "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105483.jpg?k=d381f915f7e9311bf64734cbb87742eec40a834264ba1c1237f7e18f7276060a&o=" },
                    { new Guid("241c766d-976e-4314-9207-d75208a77982"), new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), new Guid("534ed86f-6b26-4b46-8070-5be3770199f9"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105593.jpg?k=8927a514e029b26f105f355056c4332e2e103162729faae719189f4f8c9b9079&o=" },
                    { new Guid("2b84e1d5-9f79-4b47-880a-4536e911120c"), new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"), null, "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398767814.jpg?k=d9962306000f2dfa5aab8437d8b693ee3973b24b5d8e21b0acf6ea0895394d15&o=" },
                    { new Guid("2ce742c0-4174-492b-93eb-a92adf84858f"), new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), new Guid("534ed86f-6b26-4b46-8070-5be3770199f9"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105552.jpg?k=399c0e58cb01ed3c1d8f645e51855a25f31007bed9439f43119e51ccf0971b8b&o=" },
                    { new Guid("2fd4837b-34d2-475c-82b7-f652a14bd0b0"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), new Guid("135ea1ed-239f-437b-8ec7-9035dbb7f5b3"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568137.jpg?k=82e16dadd5a3d9e3c23afcd917a8b061f71d537da515aa9bac7b920088738391&o=" },
                    { new Guid("3f1ad636-2959-4d43-9ac9-ddf4f754d24e"), new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), new Guid("534ed86f-6b26-4b46-8070-5be3770199f9"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105561.jpg?k=c2d2be660be85b2dc70294c906194dc75e042342e1a928192d4dd2adb1336c84&o=" },
                    { new Guid("4a3a37b6-6053-47b4-94f2-85c8ee855890"), new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), null, "https://cf.bstatic.com/xdata/images/hotel/max1024x768/513203867.jpg?k=d2899a2b7c4e9951abb20c934dfefbcc7c7e60c025ee3feca3f7483913ab877c&o=&hp=1" },
                    { new Guid("6267856f-35b0-4082-b9b7-56916a721bfd"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), new Guid("e961fee4-03ab-4d12-a1cb-d25849b9a1c8"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642564429.jpg?k=adc6bcbad98434f49672fb7a52c62ade54673fb1866ceecf04c24ebf00691ace&o=" },
                    { new Guid("6bed0ce3-0f53-40ec-8115-6fd5bed9f78b"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), new Guid("e961fee4-03ab-4d12-a1cb-d25849b9a1c8"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568957.jpg?k=9a61b99aecfb26925d9e04b1e09b6ff791da5121f027acc671ac23ceae1f5783&o=" },
                    { new Guid("6d6da45e-e3e9-4a23-8465-e0c0f76520c1"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), new Guid("135ea1ed-239f-437b-8ec7-9035dbb7f5b3"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568077.jpg?k=90b8087fad5a98e35d8c991681db1b308ba69e3c92c44ec5e79bbfbbb4d10343&o=" },
                    { new Guid("7b357dad-3835-4b32-ba63-b54a4d583aed"), new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"), new Guid("141e8179-d711-4dec-8ec6-7ec29d47b2b2"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398788793.jpg?k=f4ea5a1232e4bc6911c85019239bdcd36fcaed2006bba78a97f9f598c4f05ce2&o=" },
                    { new Guid("7dfa6fe4-d1eb-4ed8-ae69-2ad93757b16e"), new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"), null, "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398915707.jpg?k=888ffd15c625eead51e21c81f8095e705bba850546c580cf272213135913ee16&o=" },
                    { new Guid("8964ff08-2294-4e2d-b81e-89ecd80ffb55"), new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), new Guid("534ed86f-6b26-4b46-8070-5be3770199f9"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105547.jpg?k=70acbfe097e254504627d28cb087caa803f79d54c9c93036d2b9bb954a1f2b8c&o=" },
                    { new Guid("912b8d66-354c-4a7f-9beb-0cdfb857a6f5"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), null, "https://c.ekstatic.net/dex-media/416/Five-Palm-Jumeirah-Dubai-Desktop-HotelDetails-1-3-637327495922065801.jpg" },
                    { new Guid("a127ac41-686e-4643-aa6b-89c2c29881a2"), new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"), new Guid("141e8179-d711-4dec-8ec6-7ec29d47b2b2"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398788808.jpg?k=bdc82b35918e4f921bca93cf7979baedefba3016b51d820f51b67b76d32da45b&o=" },
                    { new Guid("a181cadc-482f-4c24-8194-a26a2d02706e"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), new Guid("135ea1ed-239f-437b-8ec7-9035dbb7f5b3"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568080.jpg?k=c809ee1a5895d43812c9051d967825ba872b84157dac7163a66390991160ca09&o=" },
                    { new Guid("a7ca5e80-dff9-44ed-a14f-df8df60fb4b6"), new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"), null, "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398915704.jpg?k=331223231b2f2dcbcb9e6aecbc74f8f0b2e2020b288c150b60cfc34b447fa32e&o=" },
                    { new Guid("b51a7e4b-6efa-42e0-834e-87433b3c45c5"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), null, "https://c.ekstatic.net/dex-media/416/Five-Palm-Jumeirah-Dubai-Desktop-HotelDetails-1-1-637327495922065801.jpg" },
                    { new Guid("ba1547ec-2302-4fb8-bd03-d67b3d1a7be4"), new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), new Guid("534ed86f-6b26-4b46-8070-5be3770199f9"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105598.jpg?k=a2c196492a682c671ccafff03e52eae0bcde337f883ad6a5068082063e5c6e01&o=" },
                    { new Guid("bdc8cf17-0ef8-41dc-b35d-c1d7cc74cce7"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), new Guid("e961fee4-03ab-4d12-a1cb-d25849b9a1c8"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568942.jpg?k=e74666193c057d13d8c57edf9eaa2b0f6f0c3e6be0b31b99cf088eda27de7afc&o=" },
                    { new Guid("bdd354f6-cc31-4b49-8034-e37acbebb6a9"), new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"), null, "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398767820.jpg?k=6724f5c42976d9f85d6ea43dd55ac74f19f39236bda8a94b22172f5c73983e7d&o=&hp=1" },
                    { new Guid("bf91f7ee-8b77-4c10-9408-89f9a0b9d053"), new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), null, "https://cf.bstatic.com/xdata/images/hotel/max300/484105537.jpg?k=e47db70ecd10f46adc980421e2495483a18a256147949ec75fecc5fb108a87de&o=" },
                    { new Guid("c6ad4f65-7ff5-47e2-90b2-9229fae5feae"), new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"), null, "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398767817.jpg?k=e625a8c84d8c55e279931f86ac0bad61436de6f200203801138490f956413528&o=&hp=1" },
                    { new Guid("c74d2b15-1b87-40b1-8ea3-6bfb14e60c3f"), new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), null, "https://cf.bstatic.com/xdata/images/hotel/max500/484806261.jpg?k=82afaa65c2e8396120999c45e40bb3ec760a91a793f9f93e85abc1e678f0a7a1&o=" },
                    { new Guid("c84bac8c-64ab-4e5b-b701-a6e8ddf3fa42"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), new Guid("135ea1ed-239f-437b-8ec7-9035dbb7f5b3"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642564429.jpg?k=adc6bcbad98434f49672fb7a52c62ade54673fb1866ceecf04c24ebf00691ace&o=" },
                    { new Guid("c9316103-6786-40fc-8596-83acec72892a"), new Guid("b9ec059b-5951-4d13-9fd1-ede0802dc76e"), new Guid("141e8179-d711-4dec-8ec6-7ec29d47b2b2"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/398788815.jpg?k=a6957bfcf1b646c76ea9a2dc946898bb9d077cf8284f1056af1916a861197f8c&o=" },
                    { new Guid("d922b714-5e40-4fd1-bb21-cbec290c8e46"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), null, "https://c.ekstatic.net/dex-media/416/Five-Palm-Jumeirah-Dubai-Desktop-HotelDetails-1-2-637327495922065801.jpg" },
                    { new Guid("dc2d0612-9244-42fe-93af-6eae90d82fe4"), new Guid("bcb4f072-ecca-43c9-ab26-c060c6f364e4"), null, "https://cf.bstatic.com/xdata/images/hotel/max1024x768/484105543.jpg?k=adbc5414747997bd2c7d69ec85b08d84fd379719f210e610d60fa75220500abb&o=&hp=1" },
                    { new Guid("e947a4cf-8234-4f25-8fbb-f5dc27a6a35c"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), new Guid("e961fee4-03ab-4d12-a1cb-d25849b9a1c8"), "https://cf.bstatic.com/xdata/images/hotel/max1024x768/642568863.jpg?k=a175cad3389263f9e0bbd2460d55aeeef4fa932cbbfaabf8c581de08f9680d8d&o=" },
                    { new Guid("e9a351b1-39fe-483e-bdd7-e37dfb30ed12"), new Guid("bae99276-1865-4c63-899c-093d3b85f014"), null, "https://cf.bstatic.com/xdata/images/hotel/max1024x768/117749607.jpg?k=a7377ffe413a077a665056fa5058bdf9d3ff47000e8f703d59192535b92a198e&o=" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("0057040d-0289-4973-967c-d8f39f4c890f"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("0a4feeda-1d8f-4fa5-9ede-2b7915333691"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("14e34147-cb15-4b1c-98b8-f8e54e360f90"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("17221edc-72c8-49c4-ae5b-16a81c33a16d"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("199009c0-8e75-4d01-83cd-a6a57aeaa74a"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("19f9f6ad-57e9-4542-ae0f-59b257b1c0c0"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("241c766d-976e-4314-9207-d75208a77982"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("2b84e1d5-9f79-4b47-880a-4536e911120c"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("2ce742c0-4174-492b-93eb-a92adf84858f"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("2fd4837b-34d2-475c-82b7-f652a14bd0b0"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("3f1ad636-2959-4d43-9ac9-ddf4f754d24e"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("4a3a37b6-6053-47b4-94f2-85c8ee855890"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("6267856f-35b0-4082-b9b7-56916a721bfd"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("6bed0ce3-0f53-40ec-8115-6fd5bed9f78b"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("6d6da45e-e3e9-4a23-8465-e0c0f76520c1"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("7b357dad-3835-4b32-ba63-b54a4d583aed"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("7dfa6fe4-d1eb-4ed8-ae69-2ad93757b16e"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("8964ff08-2294-4e2d-b81e-89ecd80ffb55"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("912b8d66-354c-4a7f-9beb-0cdfb857a6f5"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("a127ac41-686e-4643-aa6b-89c2c29881a2"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("a181cadc-482f-4c24-8194-a26a2d02706e"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("a7ca5e80-dff9-44ed-a14f-df8df60fb4b6"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("b51a7e4b-6efa-42e0-834e-87433b3c45c5"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("ba1547ec-2302-4fb8-bd03-d67b3d1a7be4"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("bdc8cf17-0ef8-41dc-b35d-c1d7cc74cce7"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("bdd354f6-cc31-4b49-8034-e37acbebb6a9"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("bf91f7ee-8b77-4c10-9408-89f9a0b9d053"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("c6ad4f65-7ff5-47e2-90b2-9229fae5feae"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("c74d2b15-1b87-40b1-8ea3-6bfb14e60c3f"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("c84bac8c-64ab-4e5b-b701-a6e8ddf3fa42"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("c9316103-6786-40fc-8596-83acec72892a"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("d922b714-5e40-4fd1-bb21-cbec290c8e46"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("dc2d0612-9244-42fe-93af-6eae90d82fe4"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("e947a4cf-8234-4f25-8fbb-f5dc27a6a35c"));

            migrationBuilder.DeleteData(
                table: "PropertiesPhotos",
                keyColumn: "Id",
                keyValue: new Guid("e9a351b1-39fe-483e-bdd7-e37dfb30ed12"));

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
        }
    }
}
