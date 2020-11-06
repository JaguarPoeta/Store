using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Web.Migrations
{
    public partial class ProductosDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowId = table.Column<string>(maxLength: 50, nullable: false),
                    TableName = table.Column<string>(maxLength: 128, nullable: false),
                    Changed = table.Column<string>(maxLength: 2048, nullable: true),
                    Kind = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductoEntities",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Unidades = table.Column<int>(nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    FechaC = table.Column<DateTimeOffset>(nullable: false),
                    FechaM = table.Column<DateTimeOffset>(nullable: false),
                    UserC = table.Column<Guid>(nullable: false),
                    UserM = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProveedorEntities",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(maxLength: 50, nullable: true),
                    Telefono = table.Column<string>(maxLength: 8, nullable: true),
                    NRC = table.Column<string>(maxLength: 10, nullable: true),
                    NIT = table.Column<string>(maxLength: 14, nullable: true),
                    FechaC = table.Column<DateTimeOffset>(nullable: false),
                    UserC = table.Column<Guid>(nullable: false),
                    FechaM = table.Column<DateTimeOffset>(nullable: false),
                    UserM = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedorEntities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoEntities_Nombre",
                table: "ProductoEntities",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorEntities_NIT",
                table: "ProveedorEntities",
                column: "NIT",
                unique: true,
                filter: "[NIT] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorEntities_NRC",
                table: "ProveedorEntities",
                column: "NRC",
                unique: true,
                filter: "[NRC] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorEntities_Nombre",
                table: "ProveedorEntities",
                column: "Nombre",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditEntity");

            migrationBuilder.DropTable(
                name: "ProductoEntities");

            migrationBuilder.DropTable(
                name: "ProveedorEntities");
        }
    }
}
