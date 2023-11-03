using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenesisMegaDeskRazor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Desk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    NumberOfDrawers = table.Column<int>(type: "int", nullable: false),
                    Material = table.Column<int>(type: "int", nullable: false),
                    RushOrderDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeskQuote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseDeskPrice = table.Column<double>(type: "float", nullable: false),
                    AdditionalSqInchCost = table.Column<double>(type: "float", nullable: false),
                    DrawerCost = table.Column<double>(type: "float", nullable: false),
                    SurfaceMaterialCost = table.Column<double>(type: "float", nullable: false),
                    RushOrderCost = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    DeskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeskQuote", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Desk");

            migrationBuilder.DropTable(
                name: "DeskQuote");
        }
    }
}
