using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorusAPI.Migrations
{
    public partial class AgregarNumeroHorusTabla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroHorusDB",
                columns: table => new
                {
                    HorusNo = table.Column<int>(type: "int", nullable: false),
                    HorusId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroHorusDB", x => x.HorusNo);
                    table.ForeignKey(
                        name: "FK_NumeroHorusDB_HorusDB_HorusId",
                        column: x => x.HorusId,
                        principalTable: "HorusDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "HorusDB",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActual", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 1, 17, 20, 10, 15, 859, DateTimeKind.Local).AddTicks(7378), new DateTime(2025, 1, 17, 20, 10, 15, 859, DateTimeKind.Local).AddTicks(7365) });

            migrationBuilder.UpdateData(
                table: "HorusDB",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActual", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 1, 17, 20, 10, 15, 859, DateTimeKind.Local).AddTicks(7380), new DateTime(2025, 1, 17, 20, 10, 15, 859, DateTimeKind.Local).AddTicks(7380) });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroHorusDB_HorusId",
                table: "NumeroHorusDB",
                column: "HorusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroHorusDB");

            migrationBuilder.UpdateData(
                table: "HorusDB",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActual", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 1, 15, 18, 10, 36, 29, DateTimeKind.Local).AddTicks(7505), new DateTime(2025, 1, 15, 18, 10, 36, 29, DateTimeKind.Local).AddTicks(7491) });

            migrationBuilder.UpdateData(
                table: "HorusDB",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActual", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 1, 15, 18, 10, 36, 29, DateTimeKind.Local).AddTicks(7508), new DateTime(2025, 1, 15, 18, 10, 36, 29, DateTimeKind.Local).AddTicks(7508) });
        }
    }
}
