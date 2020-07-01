using System;
using ConstructionApp.Entity;

namespace ConstructionApp.Dto.ThongTinMeTronDto
{
    public class InputCreateTTMTDto
    {
        public Guid VehicleId { get; set; }
        public Guid HopDongId { get; set; }
        public double KhoiLuong { get; set; }
        public DateTime NgayTron { get; set; }
        public Guid MacId { get; set; }
        public static ThongTinMeTron ToEntity(InputCreateTTMTDto dto)
        {
            return new ThongTinMeTron()
            {
                NgayTron = dto.NgayTron,
                MacId = dto.MacId,
                VehicleId = dto.VehicleId,
                HopDongId = dto.HopDongId,
                KhoiLuong = dto.KhoiLuong
            };
        }
    }
}