using System;

namespace ConstructionApp.Entity
{
    public class ThanhPhanMeTronCan
    {
        public Guid Id { get; set; }
        public Guid ThongTinMeTronId { get; set; }
        public ThongTinMeTron ThongTinMeTron { get; set; }
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
    }
}