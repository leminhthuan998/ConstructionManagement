﻿using ConstructionApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Dto.VatTuDto2
{
    public class InputCreateHopDongDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static LoaiVatTu ToEntity(InputCreateHopDongDto dto)
        {
            return new LoaiVatTu()
            {
                Name = dto.Name,
                Description = dto.Description
            };
        }
    }
}