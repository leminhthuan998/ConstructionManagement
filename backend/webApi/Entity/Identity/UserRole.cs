using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp.Entity.Identity
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public User User { get; set; }

        public Role Role { get; set; }
    }
}
