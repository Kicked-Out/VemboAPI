using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VemboAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addUserTopicPeriodProgressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPeriodProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isCompleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPeriodProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPeriodProgresses_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPeriodProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTopicProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    isComplete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTopicProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTopicProgresses_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTopicProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPeriodProgresses_PeriodId",
                table: "UserPeriodProgresses",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPeriodProgresses_UserId",
                table: "UserPeriodProgresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTopicProgresses_TopicId",
                table: "UserTopicProgresses",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTopicProgresses_UserId",
                table: "UserTopicProgresses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPeriodProgresses");

            migrationBuilder.DropTable(
                name: "UserTopicProgresses");
        }
    }
}
