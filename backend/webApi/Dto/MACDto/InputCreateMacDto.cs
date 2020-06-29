using ConstructionApp.Entity;
using ConstructionApp.Service.MacService;

namespace ConstructionApp.Dto.MACDto
{
    public class InputCreateMacDto
    {
        public string MacName { get; set; }
        public double Tuoi { get; set; }
        public double? DoSut { get; set; }
        public double Cat { get; set; }
        public double XiMang { get; set; }
        public double Da { get; set; }
        public double PG { get; set; }
        public double Nuoc { get; set; }
        public string Note { get; set; }

        public static MAC ToEntity(InputCreateMacDto dto)
        {
            var entity = new MAC()
            {
                MacName = dto.MacName,
                Tuoi = dto.Tuoi,
                DoSut = dto.DoSut,
                Cat = dto.Cat,
                XiMang = dto.XiMang,
                Da = dto.Da,
                PG = dto.PG,
                Nuoc = dto.Nuoc,
                Note = dto.Note
            };
            entity.MacCode = MacService.CreateMacCode(entity);
            return entity;
        }
    }
}