using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VemboAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGuideBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_LevelType_LevelTypeId",
                table: "Levels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LevelType",
                table: "LevelType");

            migrationBuilder.RenameTable(
                name: "LevelType",
                newName: "LevelTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LevelTypes",
                table: "LevelTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_LevelTypes_LevelTypeId",
                table: "Levels",
                column: "LevelTypeId",
                principalTable: "LevelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_LevelTypes_LevelTypeId",
                table: "Levels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LevelTypes",
                table: "LevelTypes");

            migrationBuilder.RenameTable(
                name: "LevelTypes",
                newName: "LevelType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LevelType",
                table: "LevelType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_LevelType_LevelTypeId",
                table: "Levels",
                column: "LevelTypeId",
                principalTable: "LevelType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
