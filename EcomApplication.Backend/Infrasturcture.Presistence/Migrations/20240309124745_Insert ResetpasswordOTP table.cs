using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrasturcture.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class InsertResetpasswordOTPtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordOtp",
                table: "users",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordOtp",
                table: "users");
        }
    }
}
