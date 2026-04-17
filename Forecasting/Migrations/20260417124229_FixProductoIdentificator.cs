using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forecasting.Migrations
{
    /// <inheritdoc />
    public partial class FixProductoIdentificator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "identificator",
                table: "sales");

            migrationBuilder.AddColumn<string>(
                name: "identificator",
                table: "products",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "identificator",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "identificator",
                table: "sales",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
