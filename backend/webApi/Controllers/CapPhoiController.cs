using Microsoft.AspNetCore.Mvc;
using ConstructionApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ConstructionApp.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;

namespace ConstructionApp.Controllers
{
    [Authorize]
    [Route("/api/cap-phoi")]
    public class CapPhoiController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<CapPhoi> _repository;

        public CapPhoiController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _repository = _dbContext.Set<CapPhoi>();
        }

        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<CapPhoi>>))]
        public async Task<IActionResult> IndexAction()
        {
            var results = await _repository.ToListAsync();
            var newRs = new List<CapPhoi>();

            foreach(var item in results)
            {
                var thongTinMeTron = await _dbContext.Set<ThongTinMeTron>().FirstAsync(x => x.Id.Equals(item.ThongTinMeTronId));

                var mac = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == thongTinMeTron.MacId);
                var vehicle = await _dbContext.Set<Vehicle>().FirstAsync(x => x.Id == thongTinMeTron.VehicleId);
                var hopDong = await _dbContext.Set<HopDong>().FirstAsync(x => x.Id == thongTinMeTron.HopDongId);

                item.ThongTinMeTron = thongTinMeTron;
                item.ThongTinMeTron.MAC = mac;
                item.ThongTinMeTron.Vehicle = vehicle;
                item.ThongTinMeTron.HopDong = hopDong;
                newRs.Add(item);
            }

            return Ok(ApiResponse<List<CapPhoi>>.ApiOk(newRs));
        }
    }
}