using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VemboAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedUserStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Emeralds",
                table: "UserStatistics",
                newName: "VBucks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VBucks",
                table: "UserStatistics",
                newName: "Emeralds");
        }
    }
}
