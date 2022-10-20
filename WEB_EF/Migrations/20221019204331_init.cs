using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ParkingPlaces",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingPlaces", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ParkingPlaces_CarTypes",
                        column: x => x.CarType,
                        principalTable: "CarTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegNumber = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    ClientID = table.Column<int>(type: "int", nullable: true),
                    CarType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cars_CarTypes",
                        column: x => x.CarType,
                        principalTable: "CarTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Clients",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Journal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CarID = table.Column<int>(type: "int", nullable: false),
                    ParkingPlace = table.Column<int>(type: "int", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journal", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Journal_Cars",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Journal_ParkingPlaces",
                        column: x => x.ParkingPlace,
                        principalTable: "ParkingPlaces",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarType",
                table: "Cars",
                column: "CarType");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ClientID",
                table: "Cars",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "UQ__Cars__5D9A6740DCED2DC8",
                table: "Cars",
                column: "RegNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarTypes",
                table: "CarTypes",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Clients__2D535FA47897D182",
                table: "Clients",
                columns: new[] { "Name", "Surname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journal_CarID",
                table: "Journal",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_ParkingPlace",
                table: "Journal",
                column: "ParkingPlace");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPlaces_CarType",
                table: "ParkingPlaces",
                column: "CarType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Journal");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "ParkingPlaces");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "CarTypes");
        }
    }
}
