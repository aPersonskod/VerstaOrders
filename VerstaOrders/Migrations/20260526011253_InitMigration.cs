using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VerstaOrders.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderDateSequences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    CurrentValue = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDateSequences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderNumber = table.Column<string>(type: "text", nullable: false),
                    TownSender = table.Column<string>(type: "text", nullable: false),
                    AddressSender = table.Column<string>(type: "text", nullable: false),
                    TownReceiver = table.Column<string>(type: "text", nullable: false),
                    AddressReceiver = table.Column<string>(type: "text", nullable: false),
                    ProductWeight = table.Column<double>(type: "double precision", nullable: false),
                    PickupDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.InsertData(
                table: "OrderDateSequences",
                columns: new[] { "Id", "CurrentValue", "Month", "Year" },
                values: new object[] { 1, 3, 5, 2026 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "AddressReceiver", "AddressSender", "OrderNumber", "PickupDate", "ProductWeight", "TownReceiver", "TownSender" },
                values: new object[,]
                {
                    { new Guid("5c5759c6-4b4e-4150-bbe0-912c1c8f8c34"), "пр. Ленина, 100", "ул. Баумана, 5", "ORD-0520260002", new DateTime(2026, 5, 21, 21, 0, 0, 0, DateTimeKind.Utc), 8.1999999999999993, "Екатеринбург", "Казань" },
                    { new Guid("9288547b-e8f0-4d05-add5-5f29a4a3f94b"), "Океанский пр., 7", "Красный пр., 12", "ORD-0520260003", new DateTime(2026, 5, 21, 21, 0, 0, 0, DateTimeKind.Utc), 42.0, "Владивосток", "Новосибирск" },
                    { new Guid("a582d854-2216-42b7-95b9-c9b2c2d7d669"), "Невский пр., д. 25", "ул. Тверская, д. 10", "ORD-0520260001", new DateTime(2026, 5, 21, 21, 0, 0, 0, DateTimeKind.Utc), 15.5, "Санкт-Петербург", "Москва" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDateSequences");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
