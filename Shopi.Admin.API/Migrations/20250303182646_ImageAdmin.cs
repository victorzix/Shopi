using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopi.Admin.API.Migrations
{
    /// <inheritdoc />
    public partial class ImageAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AppAdmin",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AppAdmin");
        }
    }
}
