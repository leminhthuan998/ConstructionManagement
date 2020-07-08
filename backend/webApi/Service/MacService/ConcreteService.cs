using ConstructionApp.Entity;
using ConstructionApp.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Service.MacService
{
    public class ConcreteService : IConcreteService
    {
        private readonly ApplicationDbContext _dbContext;
        public ConcreteService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<ThongTinMeTron> TaoMeTron(ThongTinMeTron thongTinMeTron)
        {
            // sau khi tạo mẻ trộn thì tính cấp phối
            var capPhoi = await TinhCapPhoiChoMeTron(thongTinMeTron);
            var tinhMetronDat = await TinhThanhPhanMetronDat(thongTinMeTron);
            var tinhMeTronCan = await TinhThanhPhanMetronCan(thongTinMeTron);
            await TinhSaiSoCuaMeTron(thongTinMeTron);
            return thongTinMeTron;
        }

        public async Task<CapPhoi> TinhCapPhoiChoMeTron(ThongTinMeTron meTron)
        {
            var mac = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == meTron.MacId);
            var weight = meTron.KhoiLuong;
            var capPhoi = new CapPhoi()
            {
                Da = mac.Da,
                ThongTinMeTronId = meTron.Id,
                CatNhanTao = 0.0,
                CatSong = mac.Cat,
                XiMang = mac.XiMang,
                PhuGia1 = mac.PG,
                PhuGia2 = 0.0,
                Nuoc = mac.Nuoc,
                TiTrong = mac.Da + mac.Cat + mac.XiMang + mac.PG + mac.Nuoc,
                Check = true
            };
            await _dbContext.Set<CapPhoi>().AddAsync(capPhoi);
            await _dbContext.SaveChangesAsync();
            return capPhoi;
        }

        public async Task<SaiSo> TinhSaiSoCuaMeTron(ThongTinMeTron meTron)
        {
            var Can = await _dbContext.Set<ThanhPhanMeTronCan>().FirstAsync(x => x.ThongTinMeTronId == meTron.Id); 
            var Dat = await _dbContext.Set<ThanhPhanMeTronDat>().FirstAsync(x => x.ThongTinMeTronId == meTron.Id);
            var SaiSo = await _dbContext.Set<SaiSo>().FirstAsync(x => x.ThongTinMeTronId == meTron.Id);
            var weight = meTron.KhoiLuong;

            var daCan = Can.Da1 + Can.Da2;
            var daDat = Dat.Da1 + Dat.Da2;
            var catCan = Can.Cat1 + Can.Cat2;
            var catDat = Dat.Cat1 + Dat.Cat2;
            var xiMangDat = Dat.XiMang1 + Dat.XiMang2;
            var xiMangCan = Can.XiMang1 + Can.XiMang2;
            var phuGiaDat = Dat.PhuGia1 + Dat.PhuGia2;
            var phuGiaCan = Can.PhuGia1 + Can.PhuGia2;


            SaiSo.WCLT = Dat.Nuoc/Dat.XiMang1;
            SaiSo.WCTT = Can.Nuoc/Can.XiMang1;
            SaiSo.SALT = catDat/(catDat + daDat);
            SaiSo.SATT = catCan/(catCan + daCan);
            SaiSo.TroBay = Can.TroBay - Dat.TroBay;
            SaiSo.Da = daCan - daDat;
            SaiSo.CatSong = catCan - catDat;
            SaiSo.XiMang = xiMangCan - xiMangDat;
            SaiSo.Nuoc = Can.Nuoc - Dat.Nuoc;
            SaiSo.PhuGia1 = phuGiaCan - phuGiaDat;
            SaiSo.PhuGia2 = 0.0;

            SaiSo.Da_1m3 = SaiSo.Da/weight;
            SaiSo.CatSong_1m3 = SaiSo.CatSong/weight;
            SaiSo.XiMang_1m3 = SaiSo.XiMang/weight;
            SaiSo.PhuGia1_1m3 = SaiSo.PhuGia1/weight;
            SaiSo.PhuGia2_1m3 =SaiSo.PhuGia2/weight;

            await _dbContext.SaveChangesAsync();
                       
            return SaiSo;
        }

        public async Task<ThanhPhanMeTronCan> CapnhatThanhPhanMeTronCan(ThongTinMeTron thongTinMeTron)
        {
            var Can = await _dbContext.Set<ThanhPhanMeTronCan>().FirstAsync(x => x.ThongTinMeTronId == thongTinMeTron.Id); 
            await TinhSaiSoCuaMeTron(thongTinMeTron);
            return Can;
        }

        public async Task<ThanhPhanMeTronDat> TinhThanhPhanMetronDat(ThongTinMeTron meTron)
        {
            var mac = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == meTron.MacId);
            var weight = meTron.KhoiLuong;
            var weightDa = mac.Da * weight;
            var weightCat = mac.Cat * weight;
            var weightXiMang = mac.XiMang * weight;
            var weightNuoc = mac.Nuoc * weight;
            var weightPhuGia = mac.PG * weight;

            var xiMang1 = weightXiMang;
            var da1 = (weightDa)*0.51;
            var da2 = (weightDa)*0.49;
            switch(mac.MacCode)
            {
                case "M500":
                    xiMang1 = 0.0;
                    da2 = 0.0;
                    break;
                case "Vua75":
                    xiMang1 = 0.0;
                    da1 = 0.0;
                    da2 = 0.0;
                    break;
                case "M300":
                    da2 = 0.0;
                    break;
                case "M100":
                    da2 = 0.0;
                    break;
                case "M350":
                    da2 = 0.0;
                    break;
                case "M200":
                    da2 = 0.0;
                    break;
                case "M250":
                    da2 = 0.0;
                    break;
                default:
                    break;
            }
            var meTronDat = new ThanhPhanMeTronDat()
            {
                Da1 = da1,
                Da2 = da2,
                Cat1 = (weightCat)*0.49,
                Cat2 = (weightCat)*0.51,
                XiMang1 = xiMang1,
                XiMang2 = 0.0,
                TroBay = 0.0,
                Nuoc = weightNuoc,
                PhuGia1 = weightPhuGia,
                PhuGia2 = 0.0,
                ThongTinMeTronId = meTron.Id
            };
            await _dbContext.Set<ThanhPhanMeTronDat>().AddAsync(meTronDat);
            await _dbContext.SaveChangesAsync();
            return meTronDat;
        }

        public async Task<ThanhPhanMeTronCan> TinhThanhPhanMetronCan(ThongTinMeTron meTron)
        {
            var mac = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == meTron.MacId);
            var weight = meTron.KhoiLuong;
            var weightDa = mac.Da * weight * 1.05;
            var weightCat = mac.Cat * weight * 1.02;
            var weightXiMang = mac.XiMang * weight * 1.003;
            var weightNuoc = mac.Nuoc * weight * 0.9838;
            var weightPhuGia = mac.PG * weight * 0.98;

            var xiMang1 = weightXiMang;
            var da1 = (weightDa)*0.51;
            var da2 = (weightDa)*0.49;
            switch(mac.MacCode)
            {
                case "M500":
                    xiMang1 = 0.0;
                    da2 = 0.0;
                    break;
                case "Vua75":
                    xiMang1 = 0.0;
                    da1 = 0.0;
                    da2 = 0.0;
                    break;
                case "M300":
                    da2 = 0.0;
                    break;
                case "M100":
                    da2 = 0.0;
                    break;
                case "M350":
                    da2 = 0.0;
                    break;
                case "M200":
                    da2 = 0.0;
                    break;
                case "M250":
                    da2 = 0.0;
                    break;
                default:
                    break;
            }
            var meTronCan = new ThanhPhanMeTronCan()
            {
                Da1 = da1,
                Da2 = da2,
                Cat1 = (weightCat)*0.49,
                Cat2 = (weightCat)*0.51,
                XiMang1 = xiMang1,
                XiMang2 = 0.0,
                TroBay = 0.0,
                Nuoc = weightNuoc,
                PhuGia1 = weightPhuGia,
                PhuGia2 = 0.0,
                ThongTinMeTronId = meTron.Id
            };
            await _dbContext.Set<ThanhPhanMeTronCan>().AddAsync(meTronCan);
            await _dbContext.SaveChangesAsync();
            return meTronCan;
        }
    }
}
