using ConstructionApp.Dto.MACDto;
using ConstructionApp.Entity;
using ConstructionApp.Service.MacService;
using ConstructionApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionApp.Controllers
{
    [Authorize]
    [Route("/api/mac")]
    public class MacController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<MAC> _repository;
        public MacController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _repository = _dbContext.Set<MAC>();
        }

        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<MAC>>))]
        public async Task<IActionResult> IndexAction()
        {
            var results = await _repository.ToListAsync();
            return Ok(ApiResponse<List<MAC>>.ApiOk(results));
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<MAC>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> CreateAction([FromBody] InputCreateMacDto dto)
        {
            if(!ModelState.IsValid)
            {
                return Ok(ApiResponse<object>.ApiError(ModelState));
            }
            var newMac = InputCreateMacDto.ToEntity(dto);
            var find = await _dbContext.Set<MAC>()
              .Where(x => x.MacName.Equals(newMac.MacName) && x.Tuoi.Equals(newMac.Tuoi) && x.DoSut.Equals(newMac.DoSut)).CountAsync();
            if (find > 0)
            {
                ModelState.AddModelError(nameof(newMac.MacName), "Mac này đã được tạo trên hệ thống");
            }
            await _dbContext.Set<MAC>().AddAsync(newMac);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<MAC>.ApiOk(newMac));
        }

        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<MAC>))]
        public async Task<IActionResult> UpdateAction([FromBody] InputUpdateMacDto dto)
        {
           

            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<ModelStateDictionary>.ApiError(ModelState));
            }

            var mac = await _repository.FirstAsync(x => x.Id.Equals(dto.Id));
            InputUpdateMacDto.UpdateEntity(dto, mac);
            mac.MacCode = MacService.CreateMacCode(mac);

            // check biển số xe này đã được add hay chưa
            var find = await _dbContext.Set<MAC>()
                .Where(x => !x.Id.Equals(dto.Id) && x.MacName.Equals(dto.MacName) && x.Tuoi.Equals(dto.Tuoi) && x.DoSut.Equals(dto.DoSut)).CountAsync();
            if (find > 0)
            {
                ModelState.AddModelError(nameof(dto.MacName), "Mac này đã được tạo trên hệ thống");
            }

            _repository.Update(mac);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<MAC>.ApiOk(mac));
        }


        [HttpPost("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteAction(Guid id)
        {
            // check biển số xe này đã được add hay chưa
            var find = await _repository.Where(x => x.Id.Equals(id)).FirstAsync();
            _repository.Remove(find);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<string>.ApiOk("Xoá thành công"));
        }

    }
}