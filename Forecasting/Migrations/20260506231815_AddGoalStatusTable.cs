using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Forecasting.Migrations
{
    /// <inheritdoc />
    public partial class AddGoalStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "goals");

            migrationBuilder.AddColumn<int>(
                name: "GoalStatusStatusId",
                table: "goals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "status_id",
                table: "goals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "goalStatus",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goalStatus", x => x.status_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_goals_GoalStatusStatusId",
                table: "goals",
                column: "GoalStatusStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_goals_goalStatus_GoalStatusStatusId",
                table: "goals",
                column: "GoalStatusStatusId",
                principalTable: "goalStatus",
                principalColumn: "status_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_goals_goalStatus_GoalStatusStatusId",
                table: "goals");

            migrationBuilder.DropTable(
                name: "goalStatus");

            migrationBuilder.DropIndex(
                name: "IX_goals_GoalStatusStatusId",
                table: "goals");

            migrationBuilder.DropColumn(
                name: "GoalStatusStatusId",
                table: "goals");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "goals");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "goals",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
