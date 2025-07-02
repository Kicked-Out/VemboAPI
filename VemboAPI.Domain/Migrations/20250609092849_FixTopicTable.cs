using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VemboAPI.Domain.Migrations
{
    /// <inheritdoc />
    public partial class FixTopicTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Topics",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Topics",
                newName: "Name");
        }
    }
}
