using System.ComponentModel.DataAnnotations;

namespace Maganizer_Project.BLL.DTO
{
    public class SignInUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }     
        public string RememberMe { get; set; }
    }
}
