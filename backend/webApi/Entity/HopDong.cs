using System;
using System.Collections.Generic;

namespace ConstructionApp.Entity
{
    public class HopDong
    {
        public Guid Id { get; set; }
        public string TenHopDong { get; set; }
        public string ChuDauTu { get; set; }
        public string NhaThau { get; set; }
        public string CongTrinh { get; set; }
        public string NhaCungCapBeTong { get; set; }
        public MAC MAC { get; set; }
        public Guid MacId { get; set; }
    }
}