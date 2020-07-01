using System;
using ConstructionApp.Entity;

namespace ConstructionApp.Dto.ThongTinMeTronDto
{
    public class InputUpdateTTMTDto
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public Guid HopDongId { get; set; }
        public double KhoiLuong { get; set; }
        public DateTime NgayTron { get; set; }
        public Guid MacId { get; set; }
        public static void UpdateEntity(InputUpdateTTMTDto dto, ThongTinMeTron ttmt)
        {
            ttmt.Id = dto.Id;
            ttmt.VehicleId = dto.VehicleId;
            ttmt.HopDongId = dto.HopDongId;
            ttmt.KhoiLuong = dto.KhoiLuong;
            ttmt.NgayTron = dto.NgayTron;
            ttmt.MacId = dto.MacId;
        }
    }
}