using System;

namespace ConstructionApp.Entity
{
    public class CapPhoi
    {
        public Guid Id { get; set; }
        public Guid ThongTinMeTronId { get; set; }
        public ThongTinMeTron ThongTinMeTron { get; set; }
        public double Da { get; set; }
        public double CatNhanTao { get; set; }
        public double CatSong { get; set; }
        public double XiMang { get; set; }
        public double Nuoc { get; set; }
        public double PhuGia1 { get; set; }
        public double PhuGia2 { get; set; }
        public double TiTrong { get; set; }
        public Boolean Check { get; set; }
    }
}