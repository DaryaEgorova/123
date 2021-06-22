using Maganizer_Project.BLL.DTO;
using System;
using System.Threading.Tasks;

namespace Maganizer_Project.BLL.Interfaces
{
    public interface IUserProfileService
    {
        void UpdateProfile(EditUserProfileDTO editProfileDTO);
        Task<UserProfileDTO> GetProfile(string username);
    }
}
