using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionApp.Entity.Identity
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
    }
}
