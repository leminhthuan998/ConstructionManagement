using System;
using ConstructionApp.Entity.Identity;

namespace ConstructionApp.Entity
{
    public class ThongTinMeTron
    {
        public Guid Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public Guid VehicleId { get; set; }
        public HopDong HopDong { get; set; }
        public Guid HopDongId { get; set; }
        public double KhoiLuong { get; set; }
        public DateTime NgayTron { get; set; }
        public MAC MAC { get; set; }
        public Guid MacId { get; set; }
        public ThanhPhanMeTronCan ThanhPhanMeTronCan { get; set; }
        public ThanhPhanMeTronDat ThanhPhanMeTronDat { get; set; }
        public SaiSo SaiSo { get; set; }
        public CapPhoi CapPhoi { get; set; }
        public Guid UserId {get;set;}
        public User  User {get;set;}
    }
}