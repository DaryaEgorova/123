using System.ComponentModel.DataAnnotations;

namespace Maganizer_Project.WEB.Models
{
    public class SignInViewModel
    {   
        [Required(ErrorMessage = "Please enter your username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }   
    }
}
