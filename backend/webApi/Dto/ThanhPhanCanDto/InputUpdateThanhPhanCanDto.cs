using System;
using ConstructionApp.Entity;

namespace ConstructionApp.Dto.ThanhPhanCanDto
{
    public class InputUpdateThanhPhanCanDto
    {
        public Guid Id { get; set; }
        // public Guid ThongTinMeTronId { get; set; }
        public double Da1 { get; set; }
        public double Da2 { get; set; }
        public double Cat1 { get; set; }
        public double Cat2 { get; set; }
        public double XiMang1 { get; set; }
        public double XiMang2 { get; set; }
        public double TroBay { get; set; }
        public double Nuoc { get; set; }
        public double PhuGia1 { get; set; }
        public double PhuGia2 { get; set; }

        public static void UpdateEntity(InputUpdateThanhPhanCanDto dto, ThanhPhanMeTronCan thanhPhanMeTronCan)
        {
            thanhPhanMeTronCan.Id = dto.Id;
            // thanhPhanMeTronCan.ThongTinMeTronId = dto.Id;
            thanhPhanMeTronCan.Da1 = dto.Da1;
            thanhPhanMeTronCan.Da2 = dto.Da2;
            thanhPhanMeTronCan.Cat1 = dto.Cat1;
            thanhPhanMeTronCan.Cat2 = dto.Cat2;
            thanhPhanMeTronCan.XiMang1 = dto.XiMang1;
            thanhPhanMeTronCan.XiMang2 = dto.XiMang2;
            thanhPhanMeTronCan.TroBay = dto.TroBay;
            thanhPhanMeTronCan.Nuoc = dto.Nuoc;
            thanhPhanMeTronCan.PhuGia1 = dto.PhuGia1;
            thanhPhanMeTronCan.PhuGia2 = dto.PhuGia2;
        }
    }
}