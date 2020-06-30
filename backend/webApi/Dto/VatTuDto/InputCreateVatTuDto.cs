using ConstructionApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Dto.VatTuDto
{
    public class InputCreateVatTuDto
    {
        public string Name { get; set; }
        public DateTime InputDate { get; set; }
        public string Supplier { get; set; } //nha cung cap
        public double InputWeight { get; set; }
        public double RealWeight { get; set; }
        public Guid LoaiVatTuId { get; set; }
        public LoaiVatTu LoaiVatTu { get; set; }
        public static VatTu ToEntity(InputCreateVatTuDto dto)
        {
            return new VatTu()
            {
                Name = dto.Name,
                InputDate = dto.InputDate,
                Supplier = dto.Supplier,
                InputWeight = dto.InputWeight,
                RealWeight = dto.RealWeight,
                LoaiVatTuId = dto.LoaiVatTuId
            };
        }
    }
}
