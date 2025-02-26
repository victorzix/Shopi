using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopi.Customer.API.Migrations
{
    /// <inheritdoc />
    public partial class NametoTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Addresses",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Addresses",
                newName: "Name");
        }
    }
}
