using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorusAPI.Migrations
{
    public partial class AlimentarTablaHorus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HorusDB",
                columns: new[] { "Id", "Detalle", "FechaActual", "FechaCreacion", "ImagenUrl", "Name", "Tarifa" },
                values: new object[] { 1, "Nuevo a la venta", new DateTime(2025, 1, 15, 18, 10, 36, 29, DateTimeKind.Local).AddTicks(7505), new DateTime(2025, 1, 15, 18, 10, 36, 29, DateTimeKind.Local).AddTicks(7491), "", "Iphone", 500.0 });

            migrationBuilder.InsertData(
                table: "HorusDB",
                columns: new[] { "Id", "Detalle", "FechaActual", "FechaCreacion", "ImagenUrl", "Name", "Tarifa" },
                values: new object[] { 2, "Nueva generacion", new DateTime(2025, 1, 15, 18, 10, 36, 29, DateTimeKind.Local).AddTicks(7508), new DateTime(2025, 1, 15, 18, 10, 36, 29, DateTimeKind.Local).AddTicks(7508), "", "Samsung", 300.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HorusDB",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HorusDB",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
