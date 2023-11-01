using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenesisMegaDeskRazor.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeskId",
                table: "DeskQuote",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Desk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_DeskQuote_DeskId",
                table: "DeskQuote",
                column: "DeskId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeskQuote_Desk_DeskId",
                table: "DeskQuote",
                column: "DeskId",
                principalTable: "Desk",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeskQuote_Desk_DeskId",
                table: "DeskQuote");

            migrationBuilder.DropTable(
                name: "Desk");

            migrationBuilder.DropIndex(
                name: "IX_DeskQuote_DeskId",
                table: "DeskQuote");

            migrationBuilder.DropColumn(
                name: "DeskId",
                table: "DeskQuote");
        }
    }
}
