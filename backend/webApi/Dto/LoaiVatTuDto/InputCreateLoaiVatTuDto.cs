using ConstructionApp.Entity;

namespace ConstructionApp.Dto.VatTuDto2
{
    public class InputCreateLoaiVatTuDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static LoaiVatTu ToEntity(InputCreateLoaiVatTuDto dto)
        {
            return new LoaiVatTu()
            {
                Name = dto.Name,
                Description = dto.Description
            };
        }
    }
}
