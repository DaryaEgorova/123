using System.ComponentModel.DataAnnotations;

namespace Maganizer_Project.BLL.DTO
{
    public class SignUpUserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
