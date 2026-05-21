using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VerstaOrders.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "AddressReceiver", "AddressSender", "OrderNumber", "PickupDate", "ProductWeight", "TownReceiver", "TownSender" },
                values: new object[,]
                {
                    { new Guid("5c5759c6-4b4e-4150-bbe0-912c1c8f8c34"), "пр. Ленина, 100", "ул. Баумана, 5", "ORD-220520260002", new DateTime(2026, 5, 21, 21, 0, 0, 0, DateTimeKind.Utc), 8.1999999999999993, "Екатеринбург", "Казань" },
                    { new Guid("9288547b-e8f0-4d05-add5-5f29a4a3f94b"), "Океанский пр., 7", "Красный пр., 12", "ORD-220520260003", new DateTime(2026, 5, 21, 21, 0, 0, 0, DateTimeKind.Utc), 42.0, "Владивосток", "Новосибирск" },
                    { new Guid("a582d854-2216-42b7-95b9-c9b2c2d7d669"), "Невский пр., д. 25", "ул. Тверская, д. 10", "ORD-220520260001", new DateTime(2026, 5, 21, 21, 0, 0, 0, DateTimeKind.Utc), 15.5, "Санкт-Петербург", "Москва" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("5c5759c6-4b4e-4150-bbe0-912c1c8f8c34"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("9288547b-e8f0-4d05-add5-5f29a4a3f94b"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("a582d854-2216-42b7-95b9-c9b2c2d7d669"));
        }
    }
}
