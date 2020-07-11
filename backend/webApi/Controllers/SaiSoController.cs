using Microsoft.AspNetCore.Mvc;
using ConstructionApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ConstructionApp.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ConstructionApp.Controllers
{
    [Authorize]
    [Route("/api/sai-so")]
    public class SaiSoController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<SaiSo> _repository;

        public SaiSoController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _repository = _dbContext.Set<SaiSo>();
        }

        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<SaiSo>>))]
        public async Task<IActionResult> IndexAction()
        {
            var results = await _repository.ToListAsync();
            var newRs = new List<SaiSo>();

            foreach (var item in results)
            {
                var thongTinMeTron = await _dbContext.Set<ThongTinMeTron>().FirstAsync(x => x.Id.Equals(item.ThongTinMeTronId));

                thongTinMeTron.MAC = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == thongTinMeTron.MacId);
                thongTinMeTron.Vehicle = await _dbContext.Set<Vehicle>().FirstAsync(x => x.Id == thongTinMeTron.VehicleId);
                thongTinMeTron.HopDong = await _dbContext.Set<HopDong>().FirstAsync(x => x.Id == thongTinMeTron.HopDongId);

                item.ThongTinMeTron = thongTinMeTron;

                newRs.Add(item);
            }

            return Ok(ApiResponse<List<SaiSo>>.ApiOk(newRs));
        }

        [HttpGet("filter")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<SaiSo>>))]
        public async Task<IActionResult> FilterAction([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string macCode, [FromQuery] Guid? hopDongId)
        {
            // var results = await _repository.ToListAsync();
            var queryable = _repository.AsQueryable();
            var newRs = new List<SaiSo>();
            var results = await _repository.ToListAsync();
            // if(startDate != null && endDate == null && macCode == null && hopDongId == null)
            // {
            //     results = await _repository.Where(x => x.ThongTinMeTron.NgayTron >= startDate).ToListAsync();
            // }
            // if(startDate == null && endDate != null && macCode == null && hopDongId == null)
            // {
            //     results = await _repository.Where(x => x.ThongTinMeTron.NgayTron <= endDate).ToListAsync();
            // }
            // if(startDate == null && endDate != null && macCode != null && hopDongId == null)
            // {
            //     results = await _repository.Where(x => (x.ThongTinMeTron.NgayTron <= endDate) && (x.ThongTinMeTron.MAC.MacCode.Equals(macCode))).ToListAsync();
            // }
            // if(startDate == null && endDate != null && macCode == null && hopDongId != null)
            // {
            //     results = await _repository.Where(x => (x.ThongTinMeTron.NgayTron <= endDate) && (x.ThongTinMeTron.HopDong.Id.Equals(hopDongId))).ToListAsync();
            // }
            if ((startDate != null && endDate != null) && endDate > startDate)
            {
                results = await _repository.Where(x => x.ThongTinMeTron.NgayTron >= startDate && x.ThongTinMeTron.NgayTron <= endDate).ToListAsync();
            }
            if (startDate != null && endDate == null)
            {
                results = await _repository.Where(x => x.ThongTinMeTron.NgayTron >= startDate).ToListAsync();
            }
            if (startDate == null && endDate != null)
            {
                results = await _repository.Where(x => x.ThongTinMeTron.NgayTron <= endDate).ToListAsync();
            }
            if (startDate == null && endDate == null)
            {
                results = await _repository.ToListAsync();
            }
            var find = results.Count();
            if (find == 0)
            {
                return Ok(ApiResponse<ModelStateDictionary>.ApiError(ModelState));
            }
            else
            {
                foreach (var item in results)
                {
                    // var find1 = await _dbContext.Set<ThongTinMeTron>().Where(x => x.Id.Equals(item.ThongTinMeTronId) && x.MAC.MacCode.Equals(macCode)).CountAsync();
                    var thongTinMeTron = await _dbContext.Set<ThongTinMeTron>().FirstAsync(x => x.Id.Equals(item.ThongTinMeTronId));
            
                    if (string.IsNullOrEmpty(macCode) && hopDongId != null)
                    {
                        if(thongTinMeTron.HopDongId.Equals(hopDongId))
                        {
                            newRs.Add(item);
                        }
                    }
                    if (string.IsNullOrEmpty(macCode) && hopDongId == null)
                    {
                        newRs.Add(item);
                    }
                    if(!string.IsNullOrEmpty(macCode) && hopDongId != null)
                    {
                        var findMac = await _dbContext.Set<MAC>().Where(x => x.MacCode.Equals(macCode)).CountAsync();
                        var findHopDong = await _dbContext.Set<ThongTinMeTron>().Where(x => x.HopDongId.Equals(hopDongId)).CountAsync();
                        if(findMac > 0 && findHopDong > 0) {
                            newRs.Add(item);
                        }
                    }
                    if(!string.IsNullOrEmpty(macCode) && hopDongId == null)
                    {
                        var findMac = await _dbContext.Set<MAC>().Where(x => x.MacCode.Equals(macCode)).CountAsync();
                        if(findMac > 0)
                        {
                            newRs.Add(item);
                        }
                        // if(thongTinMeTron.MAC.MacCode.Equals(macCode))
                        // {
                        //     newRs.Add(item);
                        // }
                    }
                    // item.ThongTinMeTron = thongTinMeTron;
                    // newRs.Add(item);
                }
            }


            return Ok(ApiResponse<List<SaiSo>>.ApiOk(newRs));
        }
    }
}