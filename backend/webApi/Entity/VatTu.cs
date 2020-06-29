using System;

namespace ConstructionApp.Entity
{
    public class VatTu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime InputDate { get; set; }
        public string Supplier { get; set; } //nha cung cap
        public double InputWeight { get; set; }
        public double RealWeight { get; set; }

        public Guid LoaiVatTuId { get; set; }

        public LoaiVatTu LoaiVatTu { get; set; }
    }
}