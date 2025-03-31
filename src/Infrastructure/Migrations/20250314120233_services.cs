using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class services : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TransportCharge",
                table: "ServiceAppointments",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TransportMode",
                table: "ServiceAppointments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ServicesOffered",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ServiceCenterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesOffered", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesOffered_ServiceCenters_ServiceCenterId",
                        column: x => x.ServiceCenterId,
                        principalTable: "ServiceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAppointmentServices",
                columns: table => new
                {
                    ServiceAppointmentId = table.Column<int>(type: "integer", nullable: false),
                    ServiceOfferedId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAppointmentServices", x => new { x.ServiceAppointmentId, x.ServiceOfferedId });
                    table.ForeignKey(
                        name: "FK_ServiceAppointmentServices_ServiceAppointments_ServiceAppoi~",
                        column: x => x.ServiceAppointmentId,
                        principalTable: "ServiceAppointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceAppointmentServices_ServicesOffered_ServiceOfferedId",
                        column: x => x.ServiceOfferedId,
                        principalTable: "ServicesOffered",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAppointmentServices_ServiceOfferedId",
                table: "ServiceAppointmentServices",
                column: "ServiceOfferedId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesOffered_ServiceCenterId",
                table: "ServicesOffered",
                column: "ServiceCenterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceAppointmentServices");

            migrationBuilder.DropTable(
                name: "ServicesOffered");

            migrationBuilder.DropColumn(
                name: "TransportCharge",
                table: "ServiceAppointments");

            migrationBuilder.DropColumn(
                name: "TransportMode",
                table: "ServiceAppointments");
        }
    }
}
