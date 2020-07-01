using ConstructionApp.Authorize;
using ConstructionApp.Dto.HopDongDto;
using ConstructionApp.Entity;
using ConstructionApp.Entity.Identity;
using ConstructionApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
namespace ConstructionApp.Controllers
{
    [Authorize]
    [Route("/api/hop-dong")]
    public class HopDongController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<HopDong> _repository;

        public HopDongController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _repository = _dbContext.Set<HopDong>();
        }

        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<HopDong>>))]
        public async Task<IActionResult> IndexAction()
        {
            var results = await _repository.ToListAsync();
            var newRs = new List<HopDong>();
            foreach (var item in results)
            {
                var mac = await _dbContext.Set<MAC>()
                .FirstAsync(x => x.Id == item.MacId);
                item.MAC = mac;
                newRs.Add(item);
            }

            return Ok(ApiResponse<List<HopDong>>.ApiOk(newRs));
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<HopDong>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]

        public async Task<IActionResult> CreateAction([FromBody] InputCreateHopDongDto dto)
        {
            // check hợp đồng này đã được add hay chưa

            var find = await _dbContext.Set<HopDong>()
               .Where(x => x.TenHopDong.Equals(dto.TenHopDong) && x.ChuDauTu.Equals(dto.ChuDauTu) && x.NhaThau.Equals(dto.NhaThau)
               && x.CongTrinh.Equals(dto.CongTrinh))
               .CountAsync();
            if (find > 0)
            {
                ModelState.AddModelError(nameof(dto.TenHopDong), "Vật tư này đã được tạo trên hệ thống");
            }

            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<object>.ApiError(ModelState));
            }

            var mac = await _dbContext.Set<MAC>()
                .FirstAsync(x => x.Id == dto.MacId);
            var newHd = InputCreateHopDongDto.ToEntity(dto);
            await _dbContext.Set<HopDong>().AddAsync(newHd);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<HopDong>.ApiOk(newHd));
        }

        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<HopDong>))]
        public async Task<IActionResult> UpdateAction([FromBody] InputUpdateHopDongDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<ModelStateDictionary>.ApiError(ModelState));
            }

            var hopDong = await _repository.FirstAsync(x => x.Id.Equals(dto.Id));
            var mac = await _dbContext.Set<MAC>()
              .FirstAsync(x => x.Id == dto.MacId);
            InputUpdateHopDongDto.UpdateEntity(dto, hopDong);
            hopDong.MAC = mac;
            _repository.Update(hopDong);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<HopDong>.ApiOk(hopDong));
        }


        [HttpPost("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteAction(Guid hopDongId)
        {
            // check đã được add hay chưa
            var find = await _repository.Where(x => x.Id.Equals(hopDongId)).FirstAsync();
            _repository.Remove(find);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<string>.ApiOk("Xoá thành công"));
        }
    }
}