using System;

namespace ConstructionApp.Dto.filter
{
    public class SaiSoFilter
    {
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        #nullable enable
        public string? macCode { get; set; }
        public Guid? hopDongId { get; set; }
    }
}