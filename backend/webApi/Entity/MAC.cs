using System;

namespace ConstructionApp.Entity
{
    public class MAC
    {
        public Guid Id { get; set; }
        public string MacCode { get; set; }
        public string MacName { get; set; }
        public double Tuoi { get; set; }
        public double? DoSut { get; set; }
        public double Cat { get; set; }
        public double XiMang { get; set; }
        public double Da { get; set; }
        public double PG { get; set; }
        public double Nuoc { get; set; }
        public string Note { get; set; }
    }
}