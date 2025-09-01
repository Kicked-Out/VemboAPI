using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VemboAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "UserUnitProgresses");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "UserTopicProgresses");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "UserPeriodProgresses");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "UserLevelProgresses");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "UserLessonProgresses");

            migrationBuilder.AddColumn<int>(
                name: "CompletedCount",
                table: "UserUnitProgresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompletedCount",
                table: "UserTopicProgresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompletedCount",
                table: "UserPeriodProgresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompletedCount",
                table: "UserLevelProgresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompletedCount",
                table: "UserLessonProgresses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedCount",
                table: "UserUnitProgresses");

            migrationBuilder.DropColumn(
                name: "CompletedCount",
                table: "UserTopicProgresses");

            migrationBuilder.DropColumn(
                name: "CompletedCount",
                table: "UserPeriodProgresses");

            migrationBuilder.DropColumn(
                name: "CompletedCount",
                table: "UserLevelProgresses");

            migrationBuilder.DropColumn(
                name: "CompletedCount",
                table: "UserLessonProgresses");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "UserUnitProgresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "UserTopicProgresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "UserPeriodProgresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "UserLevelProgresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "UserLessonProgresses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
