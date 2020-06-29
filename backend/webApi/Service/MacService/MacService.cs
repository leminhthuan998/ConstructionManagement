using ConstructionApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ConstructionApp.Service.MacService
{
    public class MacService
    {
        public static string CreateMacCode(MAC entity)
        {
            return $"MAC{entity.MacName}.{entity.Tuoi}.{entity.DoSut}";
        }
    }
}
