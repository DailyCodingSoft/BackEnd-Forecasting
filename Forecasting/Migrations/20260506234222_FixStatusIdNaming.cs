using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forecasting.Migrations
{
    /// <inheritdoc />
    public partial class FixStatusIdNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_goals_goal_status_GoalStatusStatusId",
                table: "goals");

            migrationBuilder.DropIndex(
                name: "IX_goals_GoalStatusStatusId",
                table: "goals");

            migrationBuilder.DropColumn(
                name: "GoalStatusStatusId",
                table: "goals");

            migrationBuilder.RenameColumn(
                name: "status_id",
                table: "goals",
                newName: "goal_status_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "goal_status",
                newName: "goal_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_goals_goal_status_id",
                table: "goals",
                column: "goal_status_id");

            migrationBuilder.AddForeignKey(
                name: "FK_goals_goal_status_goal_status_id",
                table: "goals",
                column: "goal_status_id",
                principalTable: "goal_status",
                principalColumn: "goal_status_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_goals_goal_status_goal_status_id",
                table: "goals");

            migrationBuilder.DropIndex(
                name: "IX_goals_goal_status_id",
                table: "goals");

            migrationBuilder.RenameColumn(
                name: "goal_status_id",
                table: "goals",
                newName: "status_id");

            migrationBuilder.RenameColumn(
                name: "goal_status_id",
                table: "goal_status",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "GoalStatusStatusId",
                table: "goals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_goals_GoalStatusStatusId",
                table: "goals",
                column: "GoalStatusStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_goals_goal_status_GoalStatusStatusId",
                table: "goals",
                column: "GoalStatusStatusId",
                principalTable: "goal_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
