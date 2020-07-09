using System;
using ConstructionApp.Entity.Identity;

namespace ConstructionApp.Dto.RoleDto
{
    public class InputUpdateUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public static void UpdateEntity(InputUpdateUserDto dto, User user)
        {
            user.Id = dto.Id;
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.FullName = dto.FullName;
        }
    }
}