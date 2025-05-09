using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddDealersAndUserUpdatesV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DealerId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Dealers",
                columns: table => new
                {
                    DealerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cid = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StaticOtp = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ThemeConfig = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealers", x => x.DealerId);
                });

            migrationBuilder.InsertData(
                table: "Dealers",
                columns: new[] { "DealerId", "Cid", "Name", "StaticOtp", "ThemeConfig" },
                values: new object[,]
                {
                    { 36001, "320004", "Ford", "123456", "{\"navbarColor\": \"#0000FF\"}" },
                    { 36002, "330005", "Chevy", "123456", "{\"navbarColor\": \"#FF0000\"}" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CustomerId", "DealerId", "Email", "Name", "PasswordHash", "PhoneNumber", "Role" },
                values: new object[] { 1, "CUST123", 36001, "test@cdk.com", "Test User", "$2a$12$t8DhVKV9fZpdrXtYhWhKduZPIDsHySlUSzgq38dHgOO4wUxIbfDri", "1234567890", "Customer" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DealerId",
                table: "Users",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_Cid",
                table: "Dealers",
                column: "Cid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Dealers_DealerId",
                table: "Users",
                column: "DealerId",
                principalTable: "Dealers",
                principalColumn: "DealerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Dealers_DealerId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Dealers");

            migrationBuilder.DropIndex(
                name: "IX_Users_DealerId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DealerId",
                table: "Users");
        }
    }
}
