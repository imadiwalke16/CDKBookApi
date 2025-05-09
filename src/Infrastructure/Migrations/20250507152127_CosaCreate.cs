using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class CosaCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CustomerId", "Email", "Name", "PhoneNumber" },
                values: new object[] { "2047e667-22ad-49fa-bed4-9337eefb4023", "anuradha.yele@cdk.com", "Akanksha Agrawal", "(815) 982-7993" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CustomerId", "Email", "Name", "PhoneNumber" },
                values: new object[] { "CUST123", "test@cdk.com", "Test User", "1234567890" });
        }
    }
}
