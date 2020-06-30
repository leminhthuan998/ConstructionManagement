using ConstructionApp.Dto.VatTuDto;
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
    [Route("/api/vat-tu")]
    public class VatTuController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<VatTu> _repository;

        public VatTuController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _repository = _dbContext.Set<VatTu>();
        }

        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<VatTu>>))]
        public async Task<IActionResult> IndexAction()
        {
            var results = await _repository.ToListAsync();
            return Ok(ApiResponse<List<VatTu>>.ApiOk(results));
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<Vehicle>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> CreateAction([FromBody] InputCreateVatTuDto dto)
        {
            // check vật tư này đã được add hay chưa
            var find = await _dbContext.Set<VatTu>()
               .Where(x => x.Name.Equals(dto.Name) && x.Supplier.Equals(dto.Supplier)).CountAsync();
            if (find > 0)
            {
                ModelState.AddModelError(nameof(dto.Name), "Vật tư này đã được tạo trên hệ thống");
            }

            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<object>.ApiError(ModelState));
            }

            var newVatTu = InputCreateVatTuDto.ToEntity(dto);
            await _dbContext.Set<VatTu>().AddAsync(newVatTu);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<VatTu>.ApiOk(newVatTu));
        }

        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<VatTu>))]
        public async Task<IActionResult> UpdateAction([FromBody] InputUpdateVatTuDto dto)
        {
            // check vật tư này đã được add hay chưa
            //var find = await _dbContext.Set<VatTu>()
            //   .Where(x => x.Name.Equals(dto.Name) && x.Supplier.Equals(dto.Supplier)).CountAsync();
            //if (find > 0)
            //{
            //    ModelState.AddModelError(nameof(dto.Name), "Vật tư này đã được tạo trên hệ thống");
            //}

            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<ModelStateDictionary>.ApiError(ModelState));
            }

            var vatTu = await _repository.FirstAsync(x => x.Id.Equals(dto.Id));
            InputUpdateVatTuDto.UpdateEntity(dto, vatTu);
            _repository.Update(vatTu);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<VatTu>.ApiOk(vatTu));
        }


        [HttpPost("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteAction(Guid vatTuId)
        {
            // check vật tư này đã được add hay chưa
            var find = await _repository.Where(x => x.Id.Equals(vatTuId)).FirstAsync();
            _repository.Remove(find);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<string>.ApiOk("Xoá thành công"));
        }
    }
}