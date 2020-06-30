using ConstructionApp.Entity;
using System;


namespace ConstructionApp.Dto.HopDongDto
{
    public class InputUpdateHopDongDto
    {
        public Guid Id { get; set; }

        public string TenHopDong { get; set; }
        public string ChuDauTu { get; set; }
        public string NhaThau { get; set; }
        public string CongTrinh { get; set; }
        public string NhaCungCapBeTong { get; set; }
        public Guid MacId { get; set; }


        public static void UpdateEntity(InputUpdateHopDongDto dto, HopDong hd)
        {
            hd.Id = dto.Id;
            hd.TenHopDong = dto.TenHopDong;
            hd.ChuDauTu = dto.ChuDauTu;
            hd.NhaThau = dto.NhaThau;
            hd.CongTrinh = dto.CongTrinh;
            hd.NhaCungCapBeTong = dto.NhaCungCapBeTong;
            hd.MacId = dto.MacId;
        }
    }
}
