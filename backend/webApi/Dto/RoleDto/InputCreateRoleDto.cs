using ConstructionApp.Entity.Identity;

namespace ConstructionApp.Dto.RoleDto
{
    public class InputCreateRoleDto
    {
        public string Name { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string NormalizedName { get; set; }


        public static Role ToEntity(InputCreateRoleDto dto)
        {
            return new Role()
            {
                Name = dto.Name,
                ConcurrencyStamp = dto.ConcurrencyStamp,
                NormalizedName = dto.NormalizedName
            };
        }
    }
}