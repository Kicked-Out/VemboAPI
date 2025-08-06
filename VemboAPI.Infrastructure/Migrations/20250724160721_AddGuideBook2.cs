using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VemboAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGuideBook2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuideBookId",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GuideBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuideBooks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_GuideBookId",
                table: "Units",
                column: "GuideBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_GuideBooks_GuideBookId",
                table: "Units",
                column: "GuideBookId",
                principalTable: "GuideBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_GuideBooks_GuideBookId",
                table: "Units");

            migrationBuilder.DropTable(
                name: "GuideBooks");

            migrationBuilder.DropIndex(
                name: "IX_Units_GuideBookId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "GuideBookId",
                table: "Units");
        }
    }
}
