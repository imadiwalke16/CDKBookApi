using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddDealerCodeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Dealers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "DealerId",
                keyValue: 36001,
                column: "Code",
                value: "ABC123");

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "DealerId",
                keyValue: 36002,
                column: "Code",
                value: "XYZ789");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Dealers");
        }
    }
}
