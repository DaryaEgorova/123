using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maganizer_Project.BLL.DTO
{
    public class SignInResultDTO
    {
        public SignInResult SignInResult { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
