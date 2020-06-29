using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionApp.Migrations
{
    public partial class addEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HopDong",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenHopDong = table.Column<string>(nullable: true),
                    ChuDauTu = table.Column<string>(nullable: true),
                    NhaThau = table.Column<string>(nullable: true),
                    CongTrinh = table.Column<string>(nullable: true),
                    NhaCungCapBeTong = table.Column<string>(nullable: true),
                    MACId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HopDong_MAC_MACId",
                        column: x => x.MACId,
                        principalTable: "MAC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinMeTron",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VehicleId = table.Column<Guid>(nullable: true),
                    HopDongId = table.Column<Guid>(nullable: true),
                    KhoiLuong = table.Column<double>(nullable: false),
                    NgayTron = table.Column<DateTime>(nullable: false),
                    MACId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinMeTron", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThongTinMeTron_HopDong_HopDongId",
                        column: x => x.HopDongId,
                        principalTable: "HopDong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThongTinMeTron_MAC_MACId",
                        column: x => x.MACId,
                        principalTable: "MAC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThongTinMeTron_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CapPhoi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ThongTinMeTronId = table.Column<Guid>(nullable: false),
                    Da = table.Column<double>(nullable: false),
                    CatNhanTao = table.Column<double>(nullable: false),
                    CatSong = table.Column<double>(nullable: false),
                    XiMang = table.Column<double>(nullable: false),
                    Nuoc = table.Column<double>(nullable: false),
                    PhuGia1 = table.Column<double>(nullable: false),
                    PhuGia2 = table.Column<double>(nullable: false),
                    TiTrong = table.Column<double>(nullable: false),
                    Check = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapPhoi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CapPhoi_ThongTinMeTron_ThongTinMeTronId",
                        column: x => x.ThongTinMeTronId,
                        principalTable: "ThongTinMeTron",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaiSo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ThongTinMeTronId = table.Column<Guid>(nullable: false),
                    WCLT = table.Column<double>(nullable: false),
                    WCTT = table.Column<double>(nullable: false),
                    SALT = table.Column<double>(nullable: false),
                    SATT = table.Column<double>(nullable: false),
                    Da_1m3 = table.Column<double>(nullable: false),
                    Da = table.Column<double>(nullable: false),
                    CatSong_1m3 = table.Column<double>(nullable: false),
                    CatSong = table.Column<double>(nullable: false),
                    XiMang_1m3 = table.Column<double>(nullable: false),
                    XiMang = table.Column<double>(nullable: false),
                    TroBay = table.Column<double>(nullable: false),
                    Nuoc = table.Column<double>(nullable: false),
                    PhuGia1_1m3 = table.Column<double>(nullable: false),
                    PhuGia2_1m3 = table.Column<double>(nullable: false),
                    PhuGia1 = table.Column<double>(nullable: false),
                    PhuGia2 = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaiSo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaiSo_ThongTinMeTron_ThongTinMeTronId",
                        column: x => x.ThongTinMeTronId,
                        principalTable: "ThongTinMeTron",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThanhPhanMeTronCan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ThongTinMeTronId = table.Column<Guid>(nullable: false),
                    Da1 = table.Column<double>(nullable: false),
                    Da2 = table.Column<double>(nullable: false),
                    Cat1 = table.Column<double>(nullable: false),
                    Cat2 = table.Column<double>(nullable: false),
                    XiMang1 = table.Column<double>(nullable: false),
                    XiMang2 = table.Column<double>(nullable: false),
                    TroBay = table.Column<double>(nullable: false),
                    Nuoc = table.Column<double>(nullable: false),
                    PhuGia1 = table.Column<double>(nullable: false),
                    PhuGia2 = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhPhanMeTronCan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThanhPhanMeTronCan_ThongTinMeTron_ThongTinMeTronId",
                        column: x => x.ThongTinMeTronId,
                        principalTable: "ThongTinMeTron",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThanhPhanMeTronDat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ThongTinMeTronId = table.Column<Guid>(nullable: false),
                    Da1 = table.Column<double>(nullable: false),
                    Da2 = table.Column<double>(nullable: false),
                    Cat1 = table.Column<double>(nullable: false),
                    Cat2 = table.Column<double>(nullable: false),
                    XiMang1 = table.Column<double>(nullable: false),
                    XiMang2 = table.Column<double>(nullable: false),
                    TroBay = table.Column<double>(nullable: false),
                    Nuoc = table.Column<double>(nullable: false),
                    PhuGia1 = table.Column<double>(nullable: false),
                    PhuGia2 = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhPhanMeTronDat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThanhPhanMeTronDat_ThongTinMeTron_ThongTinMeTronId",
                        column: x => x.ThongTinMeTronId,
                        principalTable: "ThongTinMeTron",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CapPhoi_ThongTinMeTronId",
                table: "CapPhoi",
                column: "ThongTinMeTronId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HopDong_MACId",
                table: "HopDong",
                column: "MACId");

            migrationBuilder.CreateIndex(
                name: "IX_SaiSo_ThongTinMeTronId",
                table: "SaiSo",
                column: "ThongTinMeTronId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThanhPhanMeTronCan_ThongTinMeTronId",
                table: "ThanhPhanMeTronCan",
                column: "ThongTinMeTronId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThanhPhanMeTronDat_ThongTinMeTronId",
                table: "ThanhPhanMeTronDat",
                column: "ThongTinMeTronId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinMeTron_HopDongId",
                table: "ThongTinMeTron",
                column: "HopDongId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinMeTron_MACId",
                table: "ThongTinMeTron",
                column: "MACId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinMeTron_VehicleId",
                table: "ThongTinMeTron",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CapPhoi");

            migrationBuilder.DropTable(
                name: "SaiSo");

            migrationBuilder.DropTable(
                name: "ThanhPhanMeTronCan");

            migrationBuilder.DropTable(
                name: "ThanhPhanMeTronDat");

            migrationBuilder.DropTable(
                name: "ThongTinMeTron");

            migrationBuilder.DropTable(
                name: "HopDong");
        }
    }
}
