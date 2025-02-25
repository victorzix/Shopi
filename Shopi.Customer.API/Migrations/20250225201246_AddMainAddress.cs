using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopi.Customer.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMainAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Addresses",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Addresses");
        }
    }
}
