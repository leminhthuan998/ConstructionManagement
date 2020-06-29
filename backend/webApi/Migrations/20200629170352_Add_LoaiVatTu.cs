using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionApp.Migrations
{
    public partial class Add_LoaiVatTu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LoaiVatTuId",
                table: "VatTu",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "LoaiVatTu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiVatTu", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VatTu_LoaiVatTuId",
                table: "VatTu",
                column: "LoaiVatTuId");

            migrationBuilder.AddForeignKey(
                name: "FK_VatTu_LoaiVatTu_LoaiVatTuId",
                table: "VatTu",
                column: "LoaiVatTuId",
                principalTable: "LoaiVatTu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VatTu_LoaiVatTu_LoaiVatTuId",
                table: "VatTu");

            migrationBuilder.DropTable(
                name: "LoaiVatTu");

            migrationBuilder.DropIndex(
                name: "IX_VatTu_LoaiVatTuId",
                table: "VatTu");

            migrationBuilder.DropColumn(
                name: "LoaiVatTuId",
                table: "VatTu");
        }
    }
}
