using System;
using ConstructionApp.Entity;

namespace ConstructionApp.Dto.MACDto
{
    public class InputUpdateMacDto
    {
        public Guid Id { get; set; }
        public string MacName { get; set; }
        public double Tuoi { get; set; }
        public double? DoSut { get; set; }
        public double Cat { get; set; }
        public double XiMang { get; set; }
        public double Da { get; set; }
        public double PG { get; set; }
        public double Nuoc { get; set; }
        public string Note { get; set; }

        public static void UpdateEntity(InputUpdateMacDto dto, MAC mac)
        {
            mac.MacName = dto.MacName;
            mac.Tuoi = dto.Tuoi;
            mac.DoSut = dto.DoSut;
            mac.Cat = dto.Cat;
            mac.XiMang = dto.XiMang;
            mac.Da = dto.Da;
            mac.PG = dto.PG;
            mac.Nuoc = dto.Nuoc;
            mac.Note = dto.Note;
        }
    }
}