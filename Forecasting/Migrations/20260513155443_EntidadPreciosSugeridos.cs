using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Forecasting.Migrations
{
    /// <inheritdoc />
    public partial class EntidadPreciosSugeridos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "suggested_discount",
                columns: table => new
                {
                    suggested_discount_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    goal_id = table.Column<int>(type: "integer", nullable: false),
                    minimum_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    maximum_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suggested_discount", x => x.suggested_discount_id);
                    table.ForeignKey(
                        name: "FK_suggested_discount_goals_goal_id",
                        column: x => x.goal_id,
                        principalTable: "goals",
                        principalColumn: "goal_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suggested_discount_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_suggested_discount_goal_id",
                table: "suggested_discount",
                column: "goal_id");

            migrationBuilder.CreateIndex(
                name: "IX_suggested_discount_product_id",
                table: "suggested_discount",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "suggested_discount");
        }
    }
}
