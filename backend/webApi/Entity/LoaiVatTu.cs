using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Entity
{
    public class LoaiVatTu
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public List<VatTu> DanhSachNhapVatTu { get; set; }
    }
}
