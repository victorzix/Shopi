using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopi.Product.API.Migrations
{
    /// <inheritdoc />
    public partial class VisibilityToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Categories");
        }
    }
}
