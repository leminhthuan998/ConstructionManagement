using ConstructionApp;
using ConstructionApp.Dto.RoleDto;
using ConstructionApp.Entity;
using ConstructionApp.Entity.Identity;
using ConstructionApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public RoleController(ApplicationDbContext dbContext, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this._dbContext = dbContext;
            _repository = _dbContext.Set<Role>();
        }

        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<Role>>))]
        public async Task<IActionResult> IndexAction()
        {
            var results = await _repository.ToListAsync();
            return Ok(ApiResponse<List<Role>>.ApiOk(results));
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<Role>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> CreateAction([FromBody] InputCreateRoleDto dto)
        {
            var find = await _roleManager.FindByNameAsync(dto.Name);
            // var find = await _dbContext.Set<Role>().Where(x => x.Name.Equals(dto.Name)).CountAsync();
            if (find != null)
            {
                ModelState.AddModelError(nameof(dto.Name), "Role này đã được tạo trên hệ thống");
            }

            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<object>.ApiError(ModelState));
            }

            var newRole = InputCreateRoleDto.ToEntity(dto);
            var result = await _roleManager.CreateAsync(newRole);
            // await _dbContext.Set<Role>().AddAsync(newRole);
            // await _dbContext.SaveChangesAsync();
            if(result.Succeeded) {
                return Ok(ApiResponse<Role>.ApiOk(newRole));
            }
            return Ok(ApiResponse<IdentityResult>.ApiOk(result));
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

            var role = await _roleManager.FindByIdAsync(dto.Id);
            InputUpdateRoleDto.UpdateEntity(dto, role);
            var result = await _roleManager.UpdateAsync(role);
            // _repository.Update(role);
            // await _dbContext.SaveChangesAsync();
            if(result.Succeeded) {
                return Ok(ApiResponse<IdentityResult>.ApiOk(result));
            }
            return Ok(ApiResponse<Role>.ApiOk(role));
        }


        [HttpPost("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteAction(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
            }
            return Ok(ApiResponse<string>.ApiOk("Xoá thành công"));
        }

        [HttpPost("addToRole")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> AddUserToRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                var result = _userManager.AddToRoleAsync(user, roleName);
            }
            return Ok(ApiResponse<string>.ApiOk("success"));
        }

    }
}