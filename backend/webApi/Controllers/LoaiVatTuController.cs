using ConstructionApp.Authorize;
using ConstructionApp.Dto.VatTuDto2;
using ConstructionApp.Entity;
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
    [Route("/api/loai-vat-tu")]
    public class LoaiVatTuController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<LoaiVatTu> _repository;

        public LoaiVatTuController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _repository = _dbContext.Set<LoaiVatTu>();
        }

        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<LoaiVatTu>>))]
        public async Task<IActionResult> IndexAction()
        {
            var results = await _repository.ToListAsync();
            return Ok(ApiResponse<List<LoaiVatTu>>.ApiOk(results));
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<LoaiVatTu>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> CreateAction([FromBody] InputCreateLoaiVatTuDto dto)
        {
            // check loại vật tư này đã được add hay chưa
            var find = await _repository
               .Where(x => x.Name.Equals(dto.Name)).CountAsync();
            if (find > 0)
            {
                ModelState.AddModelError(nameof(dto.Name), "Vật tư này đã được tạo trên hệ thống");
            }

            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<object>.ApiError(ModelState));
            }

            var newVatTu = InputCreateLoaiVatTuDto.ToEntity(dto);
            await _dbContext.Set<LoaiVatTu>().AddAsync(newVatTu);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<LoaiVatTu>.ApiOk(newVatTu));
        }

        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<LoaiVatTu>))]
        public async Task<IActionResult> UpdateAction([FromBody] InputUpdateLoaiVatTuDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<ModelStateDictionary>.ApiError(ModelState));
            }

            var vatTu = await _repository.FirstAsync(x => x.Id.Equals(dto.Id));
            InputUpdateLoaiVatTuDto.UpdateEntity(dto, vatTu);
            _repository.Update(vatTu);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<LoaiVatTu>.ApiOk(vatTu));
        }


        [HttpPost("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteAction(Guid id)
        {
            // check vật tư này đã được add hay chưa
            var find = await _repository.Where(x => x.Id.Equals(id)).FirstAsync();
            _repository.Remove(find);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<string>.ApiOk("Xoá thành công"));
        }
    }
}