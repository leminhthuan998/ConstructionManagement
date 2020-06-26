using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConstructionApp.Service.Abstract
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        Guid Id { get; }
        string UserName { get; }
        string PhoneNumber { get; }
        string Email { get; }
        IEnumerable<string> Roles { get; }
        Claim FindClaim(string claimType);
        IEnumerable<Claim> FindClaims(string claimType);
        IEnumerable<Claim> GetAllClaims();
        bool IsInRole(string roleName);
    }
}
