using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("excel")]
        public async Task<IActionResult> ExportExcel()
        {
            var students = new[]
    {
        new {
            Id = "101", Name = "Vivek", Address = "Hyderabad"
        },
        new {
            Id = "102", Name = "Ranjeet", Address = "Hyderabad"
        },
        new {
            Id = "103", Name = "Sharath", Address = "Hyderabad"
        },
        new {
            Id = "104", Name = "Ganesh", Address = "Hyderabad"
        },
        new {
            Id = "105", Name = "Gajanan", Address = "Hyderabad"
        },
        new {
            Id = "106", Name = "Ashish", Address = "Hyderabad"
        }
    };
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
            workSheet.Cells[startRow, 2].Value = "Id";
            workSheet.Cells[startRow, 3].Value = "Name";
            workSheet.Cells[startRow, 4].Value = "Address";
            workSheet.Cells[startRow, 1].Style.Font.Bold = true;
            //Body of table  
            //  
            int recordIndex = startRow + 1;
            foreach (var student in students)
            {
                workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                workSheet.Cells[recordIndex, 2].Value = student.Id;
                workSheet.Cells[recordIndex, 3].Value = student.Name;
                workSheet.Cells[recordIndex, 4].Value = student.Address;
                recordIndex++;
            }
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();

            var memoryStream = new MemoryStream();
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
            excel.SaveAs(memoryStream);
            //memoryStream.WriteTo(Response.OutputStream);
            //Response.Flush();
            //Response.End();
            memoryStream.Seek(0, SeekOrigin.Begin);
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
