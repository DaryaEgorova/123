using System;
using System.Collections.Generic;
using System.Text;

namespace Maganizer_Project.BLL.DTO
{
    public class EmailVerificationInfoDTO
    {
        public string VerificationCode { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
