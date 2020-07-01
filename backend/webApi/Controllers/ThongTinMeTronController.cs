using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ConstructionApp.Dto.ThongTinMeTronDto;
using ConstructionApp.Entity;
using ConstructionApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ConstructionApp.Controllers
{
    [Authorize]
    [Route("/api/thong-tin-me-tron")]
    public class ThongTinMeTronController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<ThongTinMeTron> _repository;
        public ThongTinMeTronController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _repository = _dbContext.Set<ThongTinMeTron>();
        }

        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<ThongTinMeTron>>))]
        public async Task<IActionResult> IndexAction()
        {
            var results = await _repository.ToListAsync();
            var newRs = new List<ThongTinMeTron>();
            foreach (var item in results)
            {
                var mac = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == item.MacId);
                var vehicle = await _dbContext.Set<Vehicle>().FirstAsync(x => x.Id == item.VehicleId);
                var hopDong = await _dbContext.Set<HopDong>().FirstAsync(x => x.Id == item.HopDongId);
                item.MAC = mac;
                item.HopDong = hopDong;
                item.Vehicle = vehicle;
                newRs.Add(item);
            }           

            return Ok(ApiResponse<List<ThongTinMeTron>>.ApiOk(newRs));
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ThongTinMeTron>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]

        public async Task<IActionResult> CreateAction([FromBody] InputCreateTTMTDto dto)
        {
            // check thông tin mẻ trộn này đã được add hay chưa
            
            var find = await _dbContext.Set<ThongTinMeTron>().Where(x => x.VehicleId.Equals(dto.VehicleId) 
            && x.MacId.Equals(dto.MacId)
            && x.HopDongId.Equals(dto.HopDongId)
            && x.NgayTron.Equals(dto.NgayTron)
            && x.KhoiLuong.Equals(dto.KhoiLuong))
            .CountAsync();
            if (find > 0)
            {
                ModelState.AddModelError(nameof(dto.NgayTron), "Thông tin mẻ trộn này đã được tạo trên hệ thống");
            }

            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<object>.ApiError(ModelState));
            }

            // var mac = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == dto.MacId);
            // var hopDong = await _dbContext.Set<HopDong>().FirstAsync(x => x.Id == dto.HopDongId);
            // var vehicle = await _dbContext.Set<Vehicle>().FirstAsync(x => x.Id == dto.VehicleId);

            // dto.MacId = mac;
            // dto.HopDongId = hopDong;
            // dto.VehicleId = vehicle;

            var newTTMT = InputCreateTTMTDto.ToEntity(dto);
            await _dbContext.Set<ThongTinMeTron>().AddAsync(newTTMT);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<ThongTinMeTron>.ApiOk(newTTMT));
        }

        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ThongTinMeTron>))]
        public async Task<IActionResult> UpdateAction([FromBody] InputUpdateTTMTDto dto)
        {            
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<ModelStateDictionary>.ApiError(ModelState));
            }

            var thongTinMeTron = await _repository.FirstAsync(x => x.Id.Equals(dto.Id));

            var mac = await _dbContext.Set<MAC>().FirstAsync(x => x.Id == dto.MacId);
            var hopDong = await _dbContext.Set<HopDong>().FirstAsync(x => x.Id == dto.HopDongId);
            var vehicle = await _dbContext.Set<Vehicle>().FirstAsync(x => x.Id == dto.VehicleId);

            thongTinMeTron.MAC = mac;
            thongTinMeTron.HopDong = hopDong;
            thongTinMeTron.Vehicle = vehicle;

            InputUpdateTTMTDto.UpdateEntity(dto, thongTinMeTron);
            _repository.Update(thongTinMeTron);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<ThongTinMeTron>.ApiOk(thongTinMeTron));
        }


        [HttpPost("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteAction(Guid ttmtId)
        {
            // check đã được add hay chưa
            var find = await _repository.Where(x => x.Id.Equals(ttmtId)).FirstAsync();
            _repository.Remove(find);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<string>.ApiOk("Xoá thành công"));
        }
    }
}