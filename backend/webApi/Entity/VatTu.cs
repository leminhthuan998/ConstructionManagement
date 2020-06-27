using System;

namespace ConstructionApp.Entity
{
    public class VatTu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime InputDate { get; set; }
        public string Supplier { get; set; } //nha cung cap
        public double inputWeight { get; set; }
        public double realWeight { get; set; }
    }
}