using ConstructionApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Dto.VehicleDto
{
    public class InputCreateVehicleDto
    {
        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public string Description { get; set; }

        public static Vehicle ToEntity(InputCreateVehicleDto dto)
        {
            return new Vehicle()
            {
                Name = dto.Name,
                SerialNumber = dto.SerialNumber,
                Description = dto.Description,
            };
        }
    }
}
