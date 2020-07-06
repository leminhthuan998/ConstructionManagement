using ConstructionApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Service.MacService
{
    public interface IConcreteService
    {
        /// <summary>
        /// tạo mẻ trộn mới
        /// </summary>
        /// <returns></returns>
        Task<ThongTinMeTron> TaoMeTron(ThongTinMeTron thongTinMeTron);

        /// <summary>
        /// Tính cấp phối cho mẻ trộn
        /// </summary>
        /// <param name="meTron"></param>
        /// <returns></returns>
        Task<CapPhoi> TinhCapPhoiChoMeTron(ThongTinMeTron meTron);

        /// <summary>
        /// Tính ra thành phần cho mẻ trộn cần
        /// </summary>
        /// <param name="meTron"></param>
        /// <returns></returns>
        Task<ThanhPhanMeTronCan> CapnhatThanhPhanMeTronCan(ThongTinMeTron meTron);


        /// <summary>
        /// Tính thành phần thực tế mẻ trộn đặt
        /// </summary>
        /// <param name="meTron"></param>
        /// <returns></returns>
        Task<ThanhPhanMeTronDat> TinhThanhPhanMetronDat(ThongTinMeTron meTron);
        Task<ThanhPhanMeTronCan> TinhThanhPhanMetronCan(ThongTinMeTron meTron);


        /// <summary>
        /// Tính sai số của mẻ trộn
        /// </summary>
        /// <param name="meTron"></param>
        /// <returns></returns>
        Task<SaiSo> TinhSaiSoCuaMeTron(ThongTinMeTron meTron);
    }
}
