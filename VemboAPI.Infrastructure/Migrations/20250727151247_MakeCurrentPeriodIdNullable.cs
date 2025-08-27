using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VemboAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeCurrentPeriodIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStatistics_Periods_CurrentPeriodId",
                table: "UserStatistics");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentPeriodId",
                table: "UserStatistics",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStatistics_Periods_CurrentPeriodId",
                table: "UserStatistics",
                column: "CurrentPeriodId",
                principalTable: "Periods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStatistics_Periods_CurrentPeriodId",
                table: "UserStatistics");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentPeriodId",
                table: "UserStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserStatistics_Periods_CurrentPeriodId",
                table: "UserStatistics",
                column: "CurrentPeriodId",
                principalTable: "Periods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
