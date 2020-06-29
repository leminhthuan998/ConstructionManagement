using System;

namespace ConstructionApp.Entity
{
    public class SaiSo
    {
        public Guid Id { get; set; }
        public Guid ThongTinMeTronId {get;set;}
        public ThongTinMeTron ThongTinMeTron { get; set; }

        public double WCLT { get; set; }
        public double WCTT { get; set; }
        public double SALT { get; set; }
        public double SATT {get;set;}
        public double Da_1m3 {get;set;}
        public double Da {get;set;}
        public double CatSong_1m3 {get;set;}
        public double CatSong {get;set;}
        public double XiMang_1m3 {get;set;}
        public double XiMang {get;set;}
        public double TroBay {get;set;}
        public double Nuoc {get;set;}
        public double PhuGia1_1m3 {get;set;}
        public double PhuGia2_1m3 {get;set;}
        public double PhuGia1 {get;set;}
        public double PhuGia2 {get;set;}
    }
}