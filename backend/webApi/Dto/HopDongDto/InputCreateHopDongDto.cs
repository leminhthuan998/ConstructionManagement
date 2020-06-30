using ConstructionApp.Entity;
using System;

namespace ConstructionApp.Dto.HopDongDto
{
    public class InputCreateHopDongDto
    {
        public string TenHopDong { get; set; }
        public string ChuDauTu { get; set; }
        public string NhaThau { get; set; }
        public string CongTrinh { get; set; }
        public string NhaCungCapBeTong { get; set; }
        public Guid MacId { get; set; }

        public static HopDong ToEntity(InputCreateHopDongDto dto)
        {
            return new HopDong()
            {
                TenHopDong = dto.TenHopDong,
                ChuDauTu = dto.ChuDauTu,
                NhaThau = dto.NhaThau,
                CongTrinh = dto.CongTrinh,
                NhaCungCapBeTong = dto.NhaCungCapBeTong,
                MacId = dto.MacId
            };
        }
    }
}
