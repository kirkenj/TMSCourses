using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB_EF.Migrations
{
    public partial class AlterUserAddPhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Clients",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "Undefined");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Clients");
        }
    }
}
