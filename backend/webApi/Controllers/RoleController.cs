using ConstructionApp;
using ConstructionApp.Dto.RoleDto;
using ConstructionApp.Entity;
using ConstructionApp.Entity.Identity;
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
    [Route("/api/role")]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Role> _repository;
        public RoleController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
             _repository = _dbContext.Set<Role>();
        }

        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<Role>>))]
        public async Task<IActionResult> IndexAction()
        {
            // check biển số xe này đã được add hay chưa
            var results = await _repository.ToListAsync();
            return Ok(ApiResponse<List<Role>>.ApiOk(results));
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<Role>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> CreateAction([FromBody] InputCreateRoleDto dto)
        {
            // check biển số xe này đã được add hay chưa
            var find = await _dbContext.Set<Role>().Where(x => x.Name.Equals(dto.Name)).CountAsync();
            if(find > 0)
            {
                ModelState.AddModelError(nameof(dto.Name), "Role này đã được tạo trên hệ thống");
            }

            if(!ModelState.IsValid)
            {
                return Ok(ApiResponse<object>.ApiError(ModelState));
            }

            var newRole = InputCreateRoleDto.ToEntity(dto);
            await _dbContext.Set<Role>().AddAsync(newRole);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<Role>.ApiOk(newRole));
        }

        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<Role>))]
        public async Task<IActionResult> UpdateAction([FromBody] InputUpdateRoleDto dto)
        {
            var find = await _repository.Where(x => x.Name.Equals(dto.Name) && !x.Id.Equals(dto.Id)).CountAsync();
            if (find > 0)
            {
                ModelState.AddModelError(nameof(dto.Name), "Role này đã được tạo trên hệ thống");
            }

            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<ModelStateDictionary>.ApiError(ModelState));
            }

            var role = await _repository.FirstAsync(x => x.Id.Equals(dto.Id));
            InputUpdateRoleDto.UpdateEntity(dto, role);
            _repository.Update(role);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<Role>.ApiOk(role));
        }


        [HttpPost("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteAction(Guid roleId)
        {
            var find = await _repository.Where(x => x.Id.Equals(roleId)).FirstAsync();
            _repository.Remove(find);
            await _dbContext.SaveChangesAsync();
            return Ok(ApiResponse<string>.ApiOk("Xoá thành công"));
        }
    }
}