using System;
using ConstructionApp.Entity;

namespace ConstructionApp.Dto.ThongTinMeTronDto
{
    public class InputUpdateTTMTDto
    {
        public Guid Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public HopDong HopDong { get; set; }
        public double KhoiLuong { get; set; }
        public DateTime NgayTron { get; set; }
        public MAC MAC { get; set; }
        public static void UpdateEntity(InputUpdateTTMTDto dto, ThongTinMeTron ttmt)
        {
            ttmt.Id = dto.Id;
            ttmt.Vehicle = dto.Vehicle;
            ttmt.HopDong = dto.HopDong;
            ttmt.KhoiLuong = dto.KhoiLuong;
            ttmt.NgayTron = dto.NgayTron;
            ttmt.MAC = dto.MAC;
        }
    }
}