using ConstructionApp;
using ConstructionApp.Authorize;
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
        private readonly IRoleStore<Role> _roleStore;
        public RoleController(ApplicationDbContext dbContext, RoleManager<Role> roleManager, UserManager<User> userManager, IRoleStore<Role> roleStore)
        {
            this._roleStore = roleStore;
            this._userManager = userManager;
            this._roleManager = roleManager;
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

        [HttpGet("get-role-user")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<User>>))]
        public async Task<IActionResult> GetUserInRole(Guid roleId)
        {
            var userRole = await _dbContext.Set<UserRole>().Where(x => x.RoleId.Equals(roleId)).ToListAsync();
            var users = new List<User>();
            foreach(var item in userRole)
            {
                var user = await _dbContext.Set<User>().FirstAsync(x => x.Id.Equals(item.UserId));
                users.Add(user);
            }
            return Ok(ApiResponse<List<User>>.ApiOk(users));
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<Role>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> CreateAction(string roleName)
        {
            var find = await _roleManager.FindByNameAsync(roleName);
            // var find = await _dbContext.Set<Role>().Where(x => x.Name.Equals(dto.Name)).CountAsync();
            if (find != null)
            {
                ModelState.AddModelError(nameof(roleName), "Role này đã được tạo trên hệ thống");
            }

            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<object>.ApiError(ModelState));
            }
            var newRole = new Role()
            {
                Name = roleName
            };

            var result = await _roleManager.CreateAsync(newRole);
            // await _dbContext.Set<Role>().AddAsync(newRole);
            // await _dbContext.SaveChangesAsync();
            if (result.Succeeded)
            {
                return Ok(ApiResponse<Role>.ApiOk(newRole));
            }
            return Ok(ApiResponse<IdentityResult>.ApiOk(result));
        }

        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<Role>))]
        public async Task<IActionResult> UpdateAction([FromBody] InputUpdateRoleDto dto)
        {

            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<ModelStateDictionary>.ApiError(ModelState));
            }

            var dtoId = dto.Id.ToString();

            var role = await _roleManager.FindByIdAsync(dtoId);
            InputUpdateRoleDto.UpdateEntity(dto, role);
            var result = await _roleManager.UpdateAsync(role);
            // _repository.Update(role);
            // await _dbContext.SaveChangesAsync();
            if (result.Succeeded)
            {
                return Ok(ApiResponse<IdentityResult>.ApiOk(result));
            }
            return Ok(ApiResponse<Role>.ApiOk(role));
        }


        [HttpPost("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteAction(Guid Id)
        {
            var roleId = Id.ToString();
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
            }
            return Ok(ApiResponse<string>.ApiOk("Xoá thành công"));
        }

        [HttpPost("addToRole")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> AddUserToRole(Guid userId, string roleName)
        {
            // var user = await _userManager.FindByIdAsync(userId);
            // if (user != null)
            // {
            //     var result = await _userManager.AddToRoleAsync(user, roleName);
            //     if (result == null)
            //     {
            //         return Ok(ApiResponse<IdentityResult>.ApiError(result));
            //     }
            // }
            // return Ok(ApiResponse<string>.ApiOk("success"));
            var Id = userId.ToString();
            var user = await _userManager.FindByIdAsync(Id);
            await _userManager.AddToRoleAsync(user, roleName);
            return Ok();
        }


        [HttpGet("create-role-default")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRoleDefault()
        {
            await _roleManager.CreateAsync(new Role
            {
                Name = RoleConstants.MemberRole
            });
            await _roleManager.CreateAsync(new Role
            {
                Name = RoleConstants.AdministratorRole
            });
            await _roleManager.CreateAsync(new Role
            {
                Name = RoleConstants.ManagerRole
            });
            return Ok();
        }


        [HttpGet("add-user-role-admin")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        [AllowAnonymous]
        public async Task<IActionResult> AddUserRoleDefault()
        {
            var user = await _userManager.FindByNameAsync("super.admin");
            await _userManager.AddToRoleAsync(user, RoleConstants.AdministratorRole);
            return Ok();
        }
    }
}