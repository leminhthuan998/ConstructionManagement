using ConstructionApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Dto.VehicleDto
{
    public class InputUpdateVehicleDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SerialNumber { get; set; }

        public string Description { get; set; }

        public static void UpdateEntity(InputUpdateVehicleDto dto, Vehicle vehicle)
        {
            vehicle.Id = dto.Id;
            vehicle.Name = dto.Name;
            vehicle.SerialNumber = dto.SerialNumber;
            vehicle.Description = dto.Description;
        }
    }
}
