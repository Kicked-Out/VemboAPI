using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VemboAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelTypeNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelTypeId",
                table: "Levels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LevelType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Levels_LevelTypeId",
                table: "Levels",
                column: "LevelTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_LevelType_LevelTypeId",
                table: "Levels",
                column: "LevelTypeId",
                principalTable: "LevelType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_LevelType_LevelTypeId",
                table: "Levels");

            migrationBuilder.DropTable(
                name: "LevelType");

            migrationBuilder.DropIndex(
                name: "IX_Levels_LevelTypeId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "LevelTypeId",
                table: "Levels");
        }
    }
}
