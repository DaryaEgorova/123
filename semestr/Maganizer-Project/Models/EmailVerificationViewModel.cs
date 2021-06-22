using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maganizer_Project.Models
{
    public class EmailVerificationViewModel
    {
        public string VerificationCode { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
