using Maganizer_Project.DAL.EF;
using Maganizer_Project.DAL.Entities;
using Maganizer_Project.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Maganizer_Project.DAL.Repositories
{
    public class AccountRepository: IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly MaganizerContext db;
        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, MaganizerContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.db = db;
        }

        public async Task<IdentityResult> CreateAsync(UserAccount item)
        {
            var result = await userManager.CreateAsync(item.IdentityUser, item.Password);
            return result;
        }

        public async Task<SignInResult> PasswordSignInAsync(string Username, string Password, bool RememberMe)
        {
            var result = await signInManager.PasswordSignInAsync(Username, Password, RememberMe, false);        
            return result;
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<ApplicationUser> GetByName(string username)
        {
            return await userManager.FindByNameAsync(username);
        }

        public async Task<string> GetEmailConfirmationToken(ApplicationUser user)
        {
            return await userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code)
        {
            return await userManager.ConfirmEmailAsync(user, code);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return db.Users;
        }

        public void CreateMessageToAdmin(MessageToAdmin messageToAdmin)
        {
            db.MessagesToAdmin.Add(messageToAdmin);
        }

        public IEnumerable<MessageToAdmin> GetMessagesToAdmin()
        {
            return db.MessagesToAdmin.Include(x => x.ApplicationUser);
        }

        public void DeleteMessageToAdmin(int id)
        {
            MessageToAdmin item = db.MessagesToAdmin.Find(id);
            if (item != null)
            {
                db.MessagesToAdmin.Remove(item);
            }
        }

        //public void Delete(Guid id)
        //{
        //    UserAccount item = db.Accounts.Find(id);
        //    if(item != null)
        //    {
        //        db.Accounts.Remove(item);
        //    }
        //}

        //public IEnumerable<UserAccount> Find(Func<UserAccount, bool> predicate)
        //{
        //    return db.Accounts.Where(predicate).ToList();
        //}

        //public UserAccount Get(Guid id)
        //{
        //    return db.Accounts.Find(id);
        //}

        //public IEnumerable<UserAccount> GetAll()
        //{
        //    return db.Accounts;
        //}

        //public void Update(UserAccount item)
        //{
        //    db.Entry(item).State = EntityState.Modified;
        //}
    }
}
