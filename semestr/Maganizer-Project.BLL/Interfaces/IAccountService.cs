using Maganizer_Project.BLL.DTO;
using Maganizer_Project.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maganizer_Project.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<SignUpResultDTO> CreateUser(SignUpUserDTO signUpUser);
        Task<SignInResultDTO> SignInAsync(SignInUserDTO signInUser);
        Task SignOutAsync();
        Task<IdentityResult> ConfirmEmail(string userId, string code);
        Task<EmailVerificationInfoDTO> GetEmailVerificationInfo(string username);
        List<UserInfoDTO> GetInfoUsers();
        void CreateMessageToAdmin(MessageToAdminDTO message);
        List<MessageToAdminDTO> GetMessagesToAdmin();
        void DeleteMessageToAdmin(int id);

    }
}
