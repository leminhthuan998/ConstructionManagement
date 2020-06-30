using ConstructionApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Dto.VatTuDto
{
    public class InputUpdateVatTuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime InputDate { get; set; }
        public string Supplier { get; set; } //nha cung cap
        public double InputWeight { get; set; }
        public double RealWeight { get; set; }
        public Guid LoaiVatTuId { get; set; }
        public LoaiVatTu LoaiVatTu { get; set; }
        public static void UpdateEntity(InputUpdateVatTuDto dto, VatTu vt)
        {
            vt.Id = dto.Id;
            vt.Name = dto.Name;
            vt.InputDate = dto.InputDate;
            vt.Supplier = dto.Supplier;
            vt.InputWeight = dto.InputWeight;
            vt.RealWeight = dto.RealWeight;
            vt.LoaiVatTuId = dto.LoaiVatTuId;
        }
    }
}
