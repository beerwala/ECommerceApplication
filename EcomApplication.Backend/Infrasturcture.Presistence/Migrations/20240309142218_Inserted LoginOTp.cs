using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrasturcture.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class InsertedLoginOTp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoginOtp",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginOtp",
                table: "users");
        }
    }
}
