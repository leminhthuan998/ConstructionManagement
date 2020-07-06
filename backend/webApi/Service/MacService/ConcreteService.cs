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

        public Task<SaiSo> TinhSaiSoCuaMeTron(ThongTinMeTron meTron)
        {
            throw new NotImplementedException();
        }

        public async Task<ThanhPhanMeTronCan> CapnhatThanhPhanMeTronCan(ThongTinMeTron thongTinMeTron)
        {
            await TinhSaiSoCuaMeTron(thongTinMeTron);
            throw new NotImplementedException();
        }

        public async Task<ThanhPhanMeTronDat> TinhThanhPhanMetronDat(ThongTinMeTron meTron)
        {
            var mac = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == meTron.MacId);
            var weight = meTron.KhoiLuong;

            var xiMang1 = meTron.KhoiLuong * mac.XiMang;
            var da1 = (mac.Da * meTron.KhoiLuong)*0.51;
            var da2 = (mac.Da * meTron.KhoiLuong)*0.49;
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
                Cat1 = (mac.Cat * weight)*0.49,
                Cat2 = (mac.Cat * weight)*0.51,
                XiMang1 = xiMang1,
                TroBay = 0.0,
                Nuoc = mac.Nuoc * weight,
                PhuGia1 = mac.PG * weight,
                PhuGia2 = 0.0,
                ThongTinMeTronId = meTron.Id
            };
            await _dbContext.Set<ThanhPhanMeTronDat>().AddAsync(meTronDat);
            await _dbContext.SaveChangesAsync();
            return meTronDat;
        }
    }
}
