using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionApp.Migrations
{
    public partial class UpdateHopdong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HopDong_MAC_MACId",
                table: "HopDong");

            migrationBuilder.RenameColumn(
                name: "MACId",
                table: "HopDong",
                newName: "MacId");

            migrationBuilder.RenameIndex(
                name: "IX_HopDong_MACId",
                table: "HopDong",
                newName: "IX_HopDong_MacId");

            migrationBuilder.AlterColumn<Guid>(
                name: "MacId",
                table: "HopDong",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HopDong_MAC_MacId",
                table: "HopDong",
                column: "MacId",
                principalTable: "MAC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HopDong_MAC_MacId",
                table: "HopDong");

            migrationBuilder.RenameColumn(
                name: "MacId",
                table: "HopDong",
                newName: "MACId");

            migrationBuilder.RenameIndex(
                name: "IX_HopDong_MacId",
                table: "HopDong",
                newName: "IX_HopDong_MACId");

            migrationBuilder.AlterColumn<Guid>(
                name: "MACId",
                table: "HopDong",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_HopDong_MAC_MACId",
                table: "HopDong",
                column: "MACId",
                principalTable: "MAC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
