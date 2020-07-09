using ConstructionApp.Dto.RoleDto;
using ConstructionApp.Dto.UserDto;
using ConstructionApp.Entity.Identity;
using ConstructionApp.Service.Abstract;
using ConstructionApp.Utils;
using ConstructionApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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
    [Route("/api/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly ICurrentUser _currentUser;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserStore<User> _userStore;
        private readonly ApplicationDbContext _dbContext;

        public IdentityController(ICurrentUser currentUser,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUserStore<User> userStore, ApplicationDbContext dbContext
            )
        {
            this._currentUser = currentUser;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._userStore = userStore;
            this._dbContext = dbContext;
        }

        [HttpGet("check-login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<User>))]
        [AllowAnonymous]
        public async Task<IActionResult> CheckLoginAction()
        {
            if (!_currentUser.IsAuthenticated) return Ok(ApiResponse<string>.ApiError("not authenticated"));
            var user = await _userManager.FindByIdAsync(_currentUser.Id.ToString());
            var roles = await _userManager.GetRolesAsync(user);
            await Task.CompletedTask;
            return Ok(ApiResponse<User>.ApiOk(user));
        }

        [HttpGet("create-user")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<User>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IdentityResult>))]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser()
        {
            var user = new User()
            {
                UserName = "super.admin",
                Email = "mle731998@gmail.com/",
                FullName = "Le Minh Thuận"
            };
            var result = await _userManager.CreateAsync(user, "Abcdef@123");
            if(result.Succeeded)
            {
                return Ok(ApiResponse<User>.ApiOk(user));
            }
            return Ok(ApiResponse<IdentityResult>.ApiError(result));
        }

        [HttpGet("index")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<User>>))]
        public async Task<IActionResult> IndexAction()
        {
            var results = await _dbContext.Set<User>().ToListAsync();
            return Ok(ApiResponse<List<User>>.ApiOk(results));
        }

        [HttpPost("create")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<User>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IdentityResult>))]
        public async Task<IActionResult> CreateAction([FromBody] InputCreateUserDto dto)
        {
            if(!ModelState.IsValid)
            {
                return Ok(ApiResponse<object>.ApiError(ModelState));
            }
            var newUser = InputCreateUserDto.ToEntity(dto);
            var result = await _userManager.CreateAsync(newUser, "Abcdef@123");
            if(result.Succeeded)
            {
                return Ok(ApiResponse<User>.ApiOk(newUser));
            }
            return Ok(ApiResponse<IdentityResult>.ApiError(result));
        }

        [HttpPost("update")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<User>))]
        public async Task<IActionResult> UpdateAction([FromBody] InputUpdateUserDto dto)
        {

            if (!ModelState.IsValid)
            {
                return Ok(ApiResponse<ModelStateDictionary>.ApiError(ModelState));
            }

            var dtoId = dto.Id.ToString();

            var user = await _userManager.FindByIdAsync(dtoId);
            InputUpdateUserDto.UpdateEntity(dto, user);
            var result = await _userManager.UpdateAsync(user);
            // _repository.Update(role);
            // await _dbContext.SaveChangesAsync();
            if (result.Succeeded)
            {
                return Ok(ApiResponse<IdentityResult>.ApiOk(result));
            }
            return Ok(ApiResponse<User>.ApiOk(user));
        }

        [HttpPost("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<object>))]
        public async Task<IActionResult> DeleteAction(Guid Id)
        {
            var userId = Id.ToString();
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
            }
            return Ok(ApiResponse<string>.ApiOk("Xoá thành công"));
        }


        [HttpPost("/api/login")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<User>))]
        public async Task<IActionResult> LoginAction([FromBody] LoginForm form)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(form.UserName);
                try
                {
                    var result = await _signInManager.PasswordSignInAsync(form.UserName, form.Password, form.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return Ok(ApiResponse<User>.ApiOk(user));
                    } else
                    {
                        return Ok(ApiResponse<object>.ApiError(result));
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            await Task.CompletedTask;
            return Ok();
        }
        [HttpPost("/api/logout")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<User>))]
        public async Task<IActionResult> LoginAction()
        {
            if (ModelState.IsValid)
            {
                await _signInManager.SignOutAsync();
            }
            await Task.CompletedTask;
            return Ok();
        }
    }
}
