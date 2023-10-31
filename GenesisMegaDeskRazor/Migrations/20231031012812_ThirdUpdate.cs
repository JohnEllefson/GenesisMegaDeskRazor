using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenesisMegaDeskRazor.Migrations
{
    /// <inheritdoc />
    public partial class ThirdUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AdditionalSqInchCost",
                table: "DeskQuote",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BaseDeskPrice",
                table: "DeskQuote",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "DeskQuote",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "DrawerCost",
                table: "DeskQuote",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RushOrderCost",
                table: "DeskQuote",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SurfaceMaterialCost",
                table: "DeskQuote",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "DeskQuote",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalSqInchCost",
                table: "DeskQuote");

            migrationBuilder.DropColumn(
                name: "BaseDeskPrice",
                table: "DeskQuote");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "DeskQuote");

            migrationBuilder.DropColumn(
                name: "DrawerCost",
                table: "DeskQuote");

            migrationBuilder.DropColumn(
                name: "RushOrderCost",
                table: "DeskQuote");

            migrationBuilder.DropColumn(
                name: "SurfaceMaterialCost",
                table: "DeskQuote");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "DeskQuote");
        }
    }
}
