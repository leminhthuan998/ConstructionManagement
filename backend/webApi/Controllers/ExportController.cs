using ConstructionApp.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Controllers
{
    [Route("/api/export")]
    public class ExportController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<ThongTinMeTron> _repository;
        public ExportController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _repository = _dbContext.Set<ThongTinMeTron>();
        }

        [HttpGet("excel")]
        public async Task<IActionResult> ExportExcel()
        {
            var results = await _repository.ToListAsync();
            var newRs = new List<ThongTinMeTron>();
            foreach (var item in results)
            {
                var capPhoi = await _dbContext.Set<CapPhoi>().FirstAsync(x => x.ThongTinMeTronId == item.Id);
                var can = await _dbContext.Set<ThanhPhanMeTronCan>().FirstAsync(x => x.ThongTinMeTronId == item.Id);
                var dat = await _dbContext.Set<ThanhPhanMeTronDat>().FirstAsync(x => x.ThongTinMeTronId == item.Id);
                var saiso = await _dbContext.Set<SaiSo>().FirstAsync(x => x.ThongTinMeTronId == item.Id);
                var mac = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == item.MacId);
                var vehicle = await _dbContext.Set<Vehicle>().FirstAsync(x => x.Id == item.VehicleId);
                var hopDong = await _dbContext.Set<HopDong>().FirstAsync(x => x.Id == item.HopDongId);

                item.MAC = mac;
                item.HopDong = hopDong;
                item.Vehicle = vehicle;
                item.CapPhoi = capPhoi;
                item.ThanhPhanMeTronCan = can;
                item.ThanhPhanMeTronDat = dat;
                item.SaiSo = saiso;
                newRs.Add(item);
            }


            
            ExcelPackage excel = new ExcelPackage();
            var startRow = 2;
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //Header of table  
            //  
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Cells[startRow, 1].Value = "S.No";
            workSheet.Cells[startRow, 2].Value = "Ngày trộn";
            workSheet.Cells[startRow, 3].Value = "Số xe";
            workSheet.Cells[startRow, 4].Value = "Hợp đồng";
            workSheet.Cells[startRow, 5].Value = "Mac";
            workSheet.Cells[startRow, 6].Value = "Khối lượng";
            //Thành phần đặt
            //
            workSheet.Cells[startRow, 7].Value = "Đá 1_Đặt";
            workSheet.Cells[startRow, 8].Value = "Đá 2_Đặt";
            workSheet.Cells[startRow, 9].Value = "Cát 1_Đặt";
            workSheet.Cells[startRow, 10].Value = "Cát 2_Đặt";
            workSheet.Cells[startRow, 11].Value = "Xi măng 1_Đặt";
            workSheet.Cells[startRow, 12].Value = "Xi măng 2_Đặt";
            workSheet.Cells[startRow, 13].Value = "Tro bay_Đặt";
            workSheet.Cells[startRow, 14].Value = "Nước_Đặt";
            workSheet.Cells[startRow, 15].Value = "Phụ gia 1_Đặt";
            workSheet.Cells[startRow, 16].Value = "Phụ gia 2_Đặt";
            //Thành phần cân
            //
            workSheet.Cells[startRow, 17].Value = "Đá 1_Cân";
            workSheet.Cells[startRow, 18].Value = "Đá 2_Cân";
            workSheet.Cells[startRow, 19].Value = "Cát 1_Cân";
            workSheet.Cells[startRow, 20].Value = "Cát 2_Cân";
            workSheet.Cells[startRow, 21].Value = "Xi măng 1_Cân";
            workSheet.Cells[startRow, 22].Value = "Xi măng 2_Cân";
            workSheet.Cells[startRow, 23].Value = "Tro bay_Cân";
            workSheet.Cells[startRow, 24].Value = "Nước_Cân";
            workSheet.Cells[startRow, 25].Value = "Phụ gia 1_Cân";
            workSheet.Cells[startRow, 26].Value = "Phụ gia 2_Cân";
            //Cấp phối
            //
            workSheet.Cells[startRow, 27].Value = "Đá";
            // workSheet.Cells[startRow, 28].Value = "Tro bay";
            workSheet.Cells[startRow, 28].Value = "Cát nhân tạo";
            workSheet.Cells[startRow, 29].Value = "Cát sông";
            workSheet.Cells[startRow, 30].Value = "Xi măng";
            workSheet.Cells[startRow, 31].Value = "Nước";
            workSheet.Cells[startRow, 32].Value = "Phụ gia 1";
            workSheet.Cells[startRow, 33].Value = "Phụ gia 2";
            workSheet.Cells[startRow, 34].Value = "Tỉ trọng";
            //Sai số
            //
            workSheet.Cells[startRow, 35].Value = "W/C Lý thuyết";
            workSheet.Cells[startRow, 36].Value = "W/C Thực tế";
            workSheet.Cells[startRow, 37].Value = "S/A Lý thuyết";
            workSheet.Cells[startRow, 38].Value = "S/A Thực tế";
            workSheet.Cells[startRow, 39].Value = "Đá_SS_1m3";
            workSheet.Cells[startRow, 40].Value = "Cát sông_SS_1m3";
            workSheet.Cells[startRow, 41].Value = "Xi măng_SS_1m3";
            workSheet.Cells[startRow, 42].Value = "Phụ gia 1_SS_1m3";
            workSheet.Cells[startRow, 43].Value = "Phụ gia 2_SS_1m3";
            workSheet.Cells[startRow, 44].Value = "Đá_SS";
            workSheet.Cells[startRow, 45].Value = "Cát sông_SS";
            workSheet.Cells[startRow, 46].Value = "Xi măng_SS";
            workSheet.Cells[startRow, 47].Value = "Phụ gia 1_SS";
            workSheet.Cells[startRow, 48].Value = "Phụ gia 2_SS";
            workSheet.Cells[startRow, 49].Value = "Nước_SS";
            workSheet.Cells[startRow, 50].Value = "Tro bay_SS";


            workSheet.Cells[startRow, 1].Style.Font.Bold = true;
            for (int x = 1; x <= 50; x++)
            {
                if (x >= 1 && x <= 6)
                {
                    workSheet.Cells[startRow, x].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[startRow, x].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FABF8F"));
                    workSheet.Cells[startRow, x].Style.Font.Bold = true;
                    workSheet.Cells[startRow, x].Style.Font.Color.SetColor(Color.Black);
                }
                else if (x >= 7 && x <= 16)
                {
                    workSheet.Cells[startRow, x].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[startRow, x].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#C0504D"));
                    workSheet.Cells[startRow, x].Style.Font.Bold = true;
                    workSheet.Cells[startRow, x].Style.Font.Color.SetColor(Color.White);
                }
                else if (x > 16 && x <= 26)
                {
                    workSheet.Cells[startRow, x].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[startRow, x].Style.Fill.BackgroundColor.SetColor(Color.Red);
                    workSheet.Cells[startRow, x].Style.Font.Bold = true;
                    workSheet.Cells[startRow, x].Style.Font.Color.SetColor(Color.White);
                }
                else if (x > 26 && x <= 34)
                {
                    workSheet.Cells[startRow, x].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[startRow, x].Style.Fill.BackgroundColor.SetColor(Color.Cyan);
                    workSheet.Cells[startRow, x].Style.Font.Bold = true;
                    workSheet.Cells[startRow, x].Style.Font.Color.SetColor(Color.Black);
                }
                else if (x > 34 && x <= 38)
                {
                    workSheet.Cells[startRow, x].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[startRow, x].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#92D050"));
                    workSheet.Cells[startRow, x].Style.Font.Bold = true;
                    workSheet.Cells[startRow, x].Style.Font.Color.SetColor(Color.Black);
                }
                else if (x > 38 && x <= 43)
                {
                    workSheet.Cells[startRow, x].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[startRow, x].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#D8E4BC"));
                    workSheet.Cells[startRow, x].Style.Font.Bold = true;
                    workSheet.Cells[startRow, x].Style.Font.Color.SetColor(Color.Black);
                }
                else 
                {
                    workSheet.Cells[startRow, x].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[startRow, x].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#C5D9F1"));
                    workSheet.Cells[startRow, x].Style.Font.Bold = true;
                    workSheet.Cells[startRow, x].Style.Font.Color.SetColor(Color.Black);
                }

            }
            //Body of table  
            //  
            int recordIndex = startRow + 1;
            foreach (var meTron in newRs)
            {
                workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                workSheet.Cells[recordIndex, 2].Value = meTron.NgayTron;
                workSheet.Cells[recordIndex, 3].Value = meTron.Vehicle.SerialNumber;
                workSheet.Cells[recordIndex, 4].Value = meTron.HopDong.TenHopDong;
                workSheet.Cells[recordIndex, 5].Value = meTron.MAC.MacCode;
                workSheet.Cells[recordIndex, 6].Value = meTron.KhoiLuong;

                workSheet.Cells[recordIndex, 7].Value = meTron.ThanhPhanMeTronDat.Da1;
                workSheet.Cells[recordIndex, 8].Value = meTron.ThanhPhanMeTronDat.Da2;
                workSheet.Cells[recordIndex, 9].Value = meTron.ThanhPhanMeTronDat.Cat1;
                workSheet.Cells[recordIndex, 10].Value = meTron.ThanhPhanMeTronDat.Cat2;
                workSheet.Cells[recordIndex, 11].Value = meTron.ThanhPhanMeTronDat.XiMang1;
                workSheet.Cells[recordIndex, 12].Value = meTron.ThanhPhanMeTronDat.XiMang2;
                workSheet.Cells[recordIndex, 13].Value = meTron.ThanhPhanMeTronDat.TroBay;
                workSheet.Cells[recordIndex, 14].Value = meTron.ThanhPhanMeTronDat.Nuoc;
                workSheet.Cells[recordIndex, 15].Value = meTron.ThanhPhanMeTronDat.PhuGia1;
                workSheet.Cells[recordIndex, 16].Value = meTron.ThanhPhanMeTronDat.PhuGia2;

                workSheet.Cells[recordIndex, 17].Value = meTron.ThanhPhanMeTronCan.Da1;
                workSheet.Cells[recordIndex, 18].Value = meTron.ThanhPhanMeTronCan.Da2;
                workSheet.Cells[recordIndex, 19].Value = meTron.ThanhPhanMeTronCan.Cat1;
                workSheet.Cells[recordIndex, 20].Value = meTron.ThanhPhanMeTronCan.Cat2;
                workSheet.Cells[recordIndex, 21].Value = meTron.ThanhPhanMeTronCan.XiMang1;
                workSheet.Cells[recordIndex, 22].Value = meTron.ThanhPhanMeTronCan.XiMang2;
                workSheet.Cells[recordIndex, 23].Value = meTron.ThanhPhanMeTronCan.TroBay;
                workSheet.Cells[recordIndex, 24].Value = meTron.ThanhPhanMeTronCan.Nuoc;
                workSheet.Cells[recordIndex, 25].Value = meTron.ThanhPhanMeTronCan.PhuGia1;
                workSheet.Cells[recordIndex, 26].Value = meTron.ThanhPhanMeTronCan.PhuGia2;

                workSheet.Cells[recordIndex, 27].Value = meTron.CapPhoi.Da;
                workSheet.Cells[recordIndex, 28].Value = meTron.CapPhoi.CatNhanTao;
                workSheet.Cells[recordIndex, 29].Value = meTron.CapPhoi.CatSong;
                workSheet.Cells[recordIndex, 30].Value = meTron.CapPhoi.XiMang;
                workSheet.Cells[recordIndex, 31].Value = meTron.CapPhoi.Nuoc;
                workSheet.Cells[recordIndex, 32].Value = meTron.CapPhoi.PhuGia1;
                workSheet.Cells[recordIndex, 33].Value = meTron.CapPhoi.PhuGia2;
                workSheet.Cells[recordIndex, 34].Value = meTron.CapPhoi.TiTrong;

                workSheet.Cells[recordIndex, 35].Value = meTron.SaiSo.WCLT;
                workSheet.Cells[recordIndex, 36].Value = meTron.SaiSo.WCTT;
                workSheet.Cells[recordIndex, 37].Value = meTron.SaiSo.SALT;
                workSheet.Cells[recordIndex, 38].Value = meTron.SaiSo.SATT;
                workSheet.Cells[recordIndex, 39].Value = meTron.SaiSo.Da_1m3;
                workSheet.Cells[recordIndex, 40].Value = meTron.SaiSo.CatSong_1m3;
                workSheet.Cells[recordIndex, 41].Value = meTron.SaiSo.XiMang_1m3;
                workSheet.Cells[recordIndex, 42].Value = meTron.SaiSo.PhuGia1_1m3;
                workSheet.Cells[recordIndex, 43].Value = meTron.SaiSo.PhuGia2_1m3;
                workSheet.Cells[recordIndex, 44].Value = meTron.SaiSo.Da;
                workSheet.Cells[recordIndex, 45].Value = meTron.SaiSo.CatSong;
                workSheet.Cells[recordIndex, 46].Value = meTron.SaiSo.XiMang;
                workSheet.Cells[recordIndex, 47].Value = meTron.SaiSo.PhuGia1;
                workSheet.Cells[recordIndex, 48].Value = meTron.SaiSo.PhuGia2;
                workSheet.Cells[recordIndex, 49].Value = meTron.SaiSo.Nuoc;
                workSheet.Cells[recordIndex, 50].Value = meTron.SaiSo.TroBay;

                recordIndex++;
            }
            int i = 1;
            while (i <= 50)
            {
                workSheet.Column(i).AutoFit();
                i++;
            }
            // workSheet.Column(1).AutoFit();
            // workSheet.Column(2).AutoFit();
            // workSheet.Column(3).AutoFit();
            // workSheet.Column(4).AutoFit();

            var memoryStream = new MemoryStream();
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
            excel.SaveAs(memoryStream);
            //memoryStream.WriteTo(Response.OutputStream);
            //Response.Flush();
            //Response.End();
            memoryStream.Seek(0, SeekOrigin.Begin);
            string name = "test.xlsx";
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", name);
        }
    }
}
