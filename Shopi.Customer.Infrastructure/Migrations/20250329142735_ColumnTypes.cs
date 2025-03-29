using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopi.Customer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ColumnTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppCustomer_Document",
                table: "AppCustomer");

            migrationBuilder.DropIndex(
                name: "IX_AppCustomer_Email",
                table: "AppCustomer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppCustomer",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AppCustomer",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "AppCustomer",
                type: "varchar(25)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AppCustomer",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AppCustomer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppCustomer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AppCustomer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Document",
                table: "AppCustomer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)");

            migrationBuilder.CreateIndex(
                name: "IX_AppCustomer_Document",
                table: "AppCustomer",
                column: "Document",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCustomer_Email",
                table: "AppCustomer",
                column: "Email",
                unique: true);
        }
    }
}
