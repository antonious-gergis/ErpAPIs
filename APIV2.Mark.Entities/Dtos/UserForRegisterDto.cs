using System.ComponentModel.DataAnnotations;

namespace APIV2.Mark.Entities.Dtos
{
    public class UserForRegisterDto
    {
        public string? UserName { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
