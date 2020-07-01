using ConstructionApp.Entity.Identity;

namespace ConstructionApp.Dto.RoleDto
{
    public class InputCreateRoleDto
    {
        public string Name { get; set; }


        public static Role ToEntity(InputCreateRoleDto dto)
        {
            return new Role()
            {
                Name = dto.Name,
            };
        }
    }
}