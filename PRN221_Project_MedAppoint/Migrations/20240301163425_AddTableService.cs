using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN221_Project_MedAppoint.Migrations
{
    public partial class AddTableService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceID);
                });

            migrationBuilder.CreateTable(
                name: "SpecialistToServices",
                columns: table => new
                {
                    SpecialistToServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialistID = table.Column<int>(type: "int", nullable: false),
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialistToServices", x => x.SpecialistToServiceID);
                    table.ForeignKey(
                        name: "FK_SpecialistToServices_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecialistToServices_Specialists_SpecialistID",
                        column: x => x.SpecialistID,
                        principalTable: "Specialists",
                        principalColumn: "SpecialtyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialistToServices_ServiceID",
                table: "SpecialistToServices",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialistToServices_SpecialistID",
                table: "SpecialistToServices",
                column: "SpecialistID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecialistToServices");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
