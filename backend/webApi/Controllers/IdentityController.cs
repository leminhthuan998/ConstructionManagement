using ConstructionApp.Entity.Identity;
using ConstructionApp.Service.Abstract;
using ConstructionApp.Utils;
using ConstructionApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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

        public IdentityController(ICurrentUser currentUser,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUserStore<User> userStore
            )
        {
            this._currentUser = currentUser;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._userStore = userStore;
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
    }
}
