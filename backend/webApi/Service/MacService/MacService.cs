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
        /// <summary>
        /// Công thức là MAC Code = MacName + R + Tuổi + "/" + Độ sụt
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string CreateMacCode(MAC entity)
        {
            var tuoi = "";
            if(entity.Tuoi != 28)
            {
                tuoi = "R" + entity.Tuoi;
            }
            return $"M{entity.MacName}{tuoi}/{entity.DoSut}";
        }
    }
}
