using System;

namespace ConstructionApp.Entity
{
    public class ThongTinMeTron
    {
        public Guid Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public HopDong HopDong { get; set; }
        public double KhoiLuong { get; set; }
        public DateTime NgayTron { get; set; }
        public MAC MAC { get; set; }
        public ThanhPhanMeTronCan ThanhPhanMeTronCan { get; set; }
        public ThanhPhanMeTronDat ThanhPhanMeTronDat { get; set; }
        public SaiSo SaiSo { get; set; }
        public CapPhoi CapPhoi { get; set; }
    }
}