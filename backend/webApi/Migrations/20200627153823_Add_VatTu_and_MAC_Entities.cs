using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionApp.Migrations
{
    public partial class Add_VatTu_and_MAC_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MAC",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MacName = table.Column<string>(nullable: true),
                    Tuoi = table.Column<string>(nullable: true),
                    DoSut = table.Column<string>(nullable: true),
                    Cat = table.Column<double>(nullable: false),
                    XiMang = table.Column<double>(nullable: false),
                    Da = table.Column<double>(nullable: false),
                    PG = table.Column<double>(nullable: false),
                    Nuoc = table.Column<double>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VatTu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    InputDate = table.Column<DateTime>(nullable: false),
                    Supplier = table.Column<string>(nullable: true),
                    InputWeight = table.Column<double>(nullable: false),
                    RealWeight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatTu", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MAC");

            migrationBuilder.DropTable(
                name: "VatTu");
        }
    }
}
