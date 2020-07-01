using System;
using ConstructionApp.Entity.Identity;

namespace ConstructionApp.Dto.RoleDto
{
    public class InputUpdateRoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public static void UpdateEntity(InputUpdateRoleDto dto, Role role)
        {
            role.Id = dto.Id;
            role.Name = dto.Name;
        }
    }
}