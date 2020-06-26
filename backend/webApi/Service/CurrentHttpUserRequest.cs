using ConstructionApp.Service.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConstructionApp.Service
{
    public class CurrentHttpUserRequest : ICurrentUser
    {
        IHttpContextAccessor _httpContext;
        ClaimsPrincipal _principalAccessor;
        public CurrentHttpUserRequest(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _principalAccessor = _httpContext.HttpContext.User;
        }

        public Guid Id => Guid.Parse(_principalAccessor.FindFirstValue(ClaimTypes.NameIdentifier));

        public string UserName => _principalAccessor.FindFirstValue(ClaimTypes.Name);

        public string PhoneNumber => _principalAccessor.FindFirstValue(ClaimTypes.MobilePhone);

        public string Email => _principalAccessor.FindFirstValue(ClaimTypes.Email);

        public IEnumerable<string> Roles => _principalAccessor.FindAll(ClaimTypes.Role).Select(c => c.Value);

        public bool IsAuthenticated => _principalAccessor.Identity.IsAuthenticated;

        public Claim FindClaim(string claimType)
        {
            return _principalAccessor.FindFirst(claimType);
        }

        public IEnumerable<Claim> FindClaims(string claimType)
        {
            return _principalAccessor.FindAll(claimType);
        }

        public IEnumerable<Claim> GetAllClaims()
        {
            return _principalAccessor.Claims;
        }

        public bool IsInRole(string roleName)
        {
            return Roles.Any(x => x == roleName);
        }
    }
}
