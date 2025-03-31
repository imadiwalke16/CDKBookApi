using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddServiceCenterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceCenterId",
                table: "ServiceAppointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServiceCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PinCode = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    BrandsSupported = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCenters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAppointments_ServiceCenterId",
                table: "ServiceAppointments",
                column: "ServiceCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_ServiceCenters_ServiceCenterId",
                table: "ServiceAppointments",
                column: "ServiceCenterId",
                principalTable: "ServiceCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_ServiceCenters_ServiceCenterId",
                table: "ServiceAppointments");

            migrationBuilder.DropTable(
                name: "ServiceCenters");

            migrationBuilder.DropIndex(
                name: "IX_ServiceAppointments_ServiceCenterId",
                table: "ServiceAppointments");

            migrationBuilder.DropColumn(
                name: "ServiceCenterId",
                table: "ServiceAppointments");
        }
    }
}
