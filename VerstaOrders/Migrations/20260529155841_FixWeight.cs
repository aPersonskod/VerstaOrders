using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerstaOrders.Migrations
{
    /// <inheritdoc />
    public partial class FixWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProductWeight",
                table: "Orders",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("5c5759c6-4b4e-4150-bbe0-912c1c8f8c34"),
                column: "ProductWeight",
                value: 8.2m);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("9288547b-e8f0-4d05-add5-5f29a4a3f94b"),
                column: "ProductWeight",
                value: 42m);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("a582d854-2216-42b7-95b9-c9b2c2d7d669"),
                column: "ProductWeight",
                value: 15.5m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ProductWeight",
                table: "Orders",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("5c5759c6-4b4e-4150-bbe0-912c1c8f8c34"),
                column: "ProductWeight",
                value: 8.1999999999999993);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("9288547b-e8f0-4d05-add5-5f29a4a3f94b"),
                column: "ProductWeight",
                value: 42.0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("a582d854-2216-42b7-95b9-c9b2c2d7d669"),
                column: "ProductWeight",
                value: 15.5);
        }
    }
}
