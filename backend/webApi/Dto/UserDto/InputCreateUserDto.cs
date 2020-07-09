using ConstructionApp.Entity.Identity;

namespace ConstructionApp.Dto.UserDto
{
    public class InputCreateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        public static User ToEntity(InputCreateUserDto dto)
        {
            return new User()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.UserName
            };
        }
    }
}