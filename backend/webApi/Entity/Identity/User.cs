using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Entity.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
