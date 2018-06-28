using Microsoft.EntityFrameworkCore.Migrations;

namespace MuTube.Data.Migrations
{
    public partial class PassowrdString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PasswordHash",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
