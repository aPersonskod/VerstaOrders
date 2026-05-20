using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerstaOrders.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
