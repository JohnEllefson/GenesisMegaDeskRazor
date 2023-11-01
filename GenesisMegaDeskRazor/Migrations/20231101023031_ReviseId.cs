using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenesisMegaDeskRazor.Migrations
{
    /// <inheritdoc />
    public partial class ReviseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeskId",
                table: "Desk",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Desk",
                newName: "DeskId");
        }
    }
}
