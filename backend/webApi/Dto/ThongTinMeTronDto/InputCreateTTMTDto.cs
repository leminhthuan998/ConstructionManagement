using System;
using ConstructionApp.Entity;

namespace ConstructionApp.Dto.ThongTinMeTronDto
{
    public class InputCreateTTMTDto
    {
        public Vehicle Vehicle { get; set; }
        public HopDong HopDong { get; set; }
        public double KhoiLuong { get; set; }
        public DateTime NgayTron { get; set; }
        public MAC MAC { get; set; }
        public static ThongTinMeTron ToEntity(InputCreateTTMTDto dto)
        {
            return new ThongTinMeTron()
            {
                NgayTron = dto.NgayTron,
                MAC = dto.MAC,
                Vehicle = dto.Vehicle,
                HopDong = dto.HopDong,
                KhoiLuong = dto.KhoiLuong
            };
        }
    }
}