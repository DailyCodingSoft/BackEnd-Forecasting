using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forecasting.Migrations
{
    /// <inheritdoc />
    public partial class FixStatusGoalNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_goals_goalStatus_GoalStatusStatusId",
                table: "goals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_goalStatus",
                table: "goalStatus");

            migrationBuilder.RenameTable(
                name: "goalStatus",
                newName: "goal_status");

            migrationBuilder.RenameColumn(
                name: "status_id",
                table: "goal_status",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_goal_status",
                table: "goal_status",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_goals_goal_status_GoalStatusStatusId",
                table: "goals",
                column: "GoalStatusStatusId",
                principalTable: "goal_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_goals_goal_status_GoalStatusStatusId",
                table: "goals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_goal_status",
                table: "goal_status");

            migrationBuilder.RenameTable(
                name: "goal_status",
                newName: "goalStatus");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "goalStatus",
                newName: "status_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_goalStatus",
                table: "goalStatus",
                column: "status_id");

            migrationBuilder.AddForeignKey(
                name: "FK_goals_goalStatus_GoalStatusStatusId",
                table: "goals",
                column: "GoalStatusStatusId",
                principalTable: "goalStatus",
                principalColumn: "status_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
