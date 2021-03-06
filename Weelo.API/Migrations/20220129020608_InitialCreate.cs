using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Weelo.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    IdOwner = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.IdOwner);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    IdProperty = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CodeInternal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerIdOwner = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.IdProperty);
                    table.ForeignKey(
                        name: "FK_Properties_Owners_OwnerIdOwner",
                        column: x => x.OwnerIdOwner,
                        principalTable: "Owners",
                        principalColumn: "IdOwner",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                columns: table => new
                {
                    IdPropertyImage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    File = table.Column<byte>(type: "tinyint", nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    PropertyIdProperty = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.IdPropertyImage);
                    table.ForeignKey(
                        name: "FK_PropertyImages_Properties_PropertyIdProperty",
                        column: x => x.PropertyIdProperty,
                        principalTable: "Properties",
                        principalColumn: "IdProperty",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTraces",
                columns: table => new
                {
                    IdPropertyTrace = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateSale = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyIdProperty = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTraces", x => x.IdPropertyTrace);
                    table.ForeignKey(
                        name: "FK_PropertyTraces_Properties_PropertyIdProperty",
                        column: x => x.PropertyIdProperty,
                        principalTable: "Properties",
                        principalColumn: "IdProperty",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerIdOwner",
                table: "Properties",
                column: "OwnerIdOwner");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_PropertyIdProperty",
                table: "PropertyImages",
                column: "PropertyIdProperty");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTraces_PropertyIdProperty",
                table: "PropertyTraces",
                column: "PropertyIdProperty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyImages");

            migrationBuilder.DropTable(
                name: "PropertyTraces");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
