using ConstructionApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Dto.VatTuDto2
{
    public class InputUpdateLoaiVatTuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public static void UpdateEntity(InputUpdateLoaiVatTuDto dto, LoaiVatTu vt)
        {
            vt.Id = dto.Id;
            vt.Name = dto.Name;
            vt.Description = dto.Description;
        }
    }
}
