using System;
using ConstructionApp.Entity.Identity;

namespace ConstructionApp.Dto.RoleDto
{
    public class InputUpdateRoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string NormalizedName { get; set; }
        public static void UpdateEntity(InputUpdateRoleDto dto, Role role)
        {
            role.Name = dto.Name;
            role.ConcurrencyStamp = dto.ConcurrencyStamp;
            role.NormalizedName = dto.NormalizedName;
        }
    }
}