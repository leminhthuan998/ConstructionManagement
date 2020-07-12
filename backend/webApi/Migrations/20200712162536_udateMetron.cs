using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionApp.Migrations
{
    public partial class udateMetron : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThongTinMeTron_HopDong_HopDongId",
                table: "ThongTinMeTron");

            migrationBuilder.DropForeignKey(
                name: "FK_ThongTinMeTron_MAC_MACId",
                table: "ThongTinMeTron");

            migrationBuilder.DropForeignKey(
                name: "FK_ThongTinMeTron_Vehicle_VehicleId",
                table: "ThongTinMeTron");

            migrationBuilder.RenameColumn(
                name: "MACId",
                table: "ThongTinMeTron",
                newName: "MacId");

            migrationBuilder.RenameIndex(
                name: "IX_ThongTinMeTron_MACId",
                table: "ThongTinMeTron",
                newName: "IX_ThongTinMeTron_MacId");

            migrationBuilder.AlterColumn<Guid>(
                name: "VehicleId",
                table: "ThongTinMeTron",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MacId",
                table: "ThongTinMeTron",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "HopDongId",
                table: "ThongTinMeTron",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ThongTinMeTron",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinMeTron_UserId",
                table: "ThongTinMeTron",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThongTinMeTron_HopDong_HopDongId",
                table: "ThongTinMeTron",
                column: "HopDongId",
                principalTable: "HopDong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThongTinMeTron_MAC_MacId",
                table: "ThongTinMeTron",
                column: "MacId",
                principalTable: "MAC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThongTinMeTron_Users_UserId",
                table: "ThongTinMeTron",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThongTinMeTron_Vehicle_VehicleId",
                table: "ThongTinMeTron",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThongTinMeTron_HopDong_HopDongId",
                table: "ThongTinMeTron");

            migrationBuilder.DropForeignKey(
                name: "FK_ThongTinMeTron_MAC_MacId",
                table: "ThongTinMeTron");

            migrationBuilder.DropForeignKey(
                name: "FK_ThongTinMeTron_Users_UserId",
                table: "ThongTinMeTron");

            migrationBuilder.DropForeignKey(
                name: "FK_ThongTinMeTron_Vehicle_VehicleId",
                table: "ThongTinMeTron");

            migrationBuilder.DropIndex(
                name: "IX_ThongTinMeTron_UserId",
                table: "ThongTinMeTron");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ThongTinMeTron");

            migrationBuilder.RenameColumn(
                name: "MacId",
                table: "ThongTinMeTron",
                newName: "MACId");

            migrationBuilder.RenameIndex(
                name: "IX_ThongTinMeTron_MacId",
                table: "ThongTinMeTron",
                newName: "IX_ThongTinMeTron_MACId");

            migrationBuilder.AlterColumn<Guid>(
                name: "VehicleId",
                table: "ThongTinMeTron",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "MACId",
                table: "ThongTinMeTron",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "HopDongId",
                table: "ThongTinMeTron",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_ThongTinMeTron_HopDong_HopDongId",
                table: "ThongTinMeTron",
                column: "HopDongId",
                principalTable: "HopDong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThongTinMeTron_MAC_MACId",
                table: "ThongTinMeTron",
                column: "MACId",
                principalTable: "MAC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThongTinMeTron_Vehicle_VehicleId",
                table: "ThongTinMeTron",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
