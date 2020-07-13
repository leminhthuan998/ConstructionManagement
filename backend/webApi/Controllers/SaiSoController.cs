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

        [HttpPost("filter")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<SaiSo>>))]
        public async Task<IActionResult> FilterAction([FromQuery] string start, [FromQuery] string end, [FromQuery] string macId, [FromQuery] string hdId)
        {
            // var results = await _repository.ToListAsync();
            DateTime? startDate;
            DateTime? endDate;
            Guid? hopDongId;
            Guid? macCode;

            //check hdid
            if(string.IsNullOrEmpty(hdId))
            {
                hopDongId = null;
            }
            else {
                hopDongId = Guid.Parse(hdId);
            }

            //check macid
            if(string.IsNullOrEmpty(macId))
            {
                macCode = null;
            }
            else {
                macCode = Guid.Parse(macId);
            }

            //check start
            if(string.IsNullOrEmpty(start))
            {
                startDate = null;
            }
            else {
                startDate = DateTime.Parse(start);
            }

            //check end
            if(string.IsNullOrEmpty(end))
            {
                endDate = null;
            }
            else {
                endDate = DateTime.Parse(end);
            }

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
            
                    if (macCode == null && hopDongId != null)
                    {
                        if(thongTinMeTron.HopDongId.Equals(hopDongId))
                        {
                            newRs.Add(item);
                        }
                    }
                    if (macCode == null && hopDongId == null)
                    {
                        newRs.Add(item);
                    }
                    if(macCode != null && hopDongId != null)
                    {
                        var mac = await _dbContext.Set<MAC>().Where(x => x.Id.Equals(item.ThongTinMeTron.MacId)).FirstAsync();
                        item.ThongTinMeTron.MAC = mac;
                        // var findHopDong = await _dbContext.Set<ThongTinMeTron>().Where(x => x.HopDongId.Equals(hopDongId)).CountAsync();
                        if(item.ThongTinMeTron.MacId.Equals(macCode) && item.ThongTinMeTron.HopDongId.Equals(hopDongId)) {
                            newRs.Add(item);
                        }
                    }
                    if(macCode != null && hopDongId == null)
                    {
                        var mac = await _dbContext.Set<MAC>().Where(x => x.Id.Equals(item.ThongTinMeTron.MacId)).FirstAsync();
                        item.ThongTinMeTron.MAC = mac;
                        // var findMac = await _dbContext.Set<MAC>().Where(x => x.Id.Equals(item.ThongTinMeTron.MacId)).CountAsync();
                        // var findMac =  item.ThongTinMeTron.MAC.MacCode.Equals(macCode);
                        if(item.ThongTinMeTron.MacId.Equals(macCode))
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