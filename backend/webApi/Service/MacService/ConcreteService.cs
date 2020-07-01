using ConstructionApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Service.MacService
{
    public class ConcreteService : IConcreteService
    {
        public async Task<ThongTinMeTron> TaoMeTron(ThongTinMeTron thongTinMeTron)
        {
            // sau khi tạo mẻ trộn thì tính cấp phối
            var capPhoi = await TinhCapPhoiChoMeTron(thongTinMeTron);
            var tinhMetronDat = await TinhThanhPhanMetronDat(thongTinMeTron);
            throw new NotImplementedException();
        }

        public Task<CapPhoi> TinhCapPhoiChoMeTron(ThongTinMeTron meTron)
        {
            throw new NotImplementedException();
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

        public Task<ThanhPhanMeTronDat> TinhThanhPhanMetronDat(ThongTinMeTron meTron)
        {
            throw new NotImplementedException();
        }
    }
}
