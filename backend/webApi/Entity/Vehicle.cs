using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Entity
{
    public class Vehicle
    {
        public Guid Id { get; set; }

        // tên xe -> nếu có
        public string Name { get; set; }

        // bảng số xe
        public string SerialNumber { get; set; }


        public string Description { get; set; }
    }
}
