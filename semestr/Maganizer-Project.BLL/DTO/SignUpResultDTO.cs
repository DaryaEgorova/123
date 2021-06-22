using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maganizer_Project.BLL.DTO
{
    public class SignUpResultDTO
    {
        public IdentityResult Result { get; set; }
        public string VerificationCode { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
