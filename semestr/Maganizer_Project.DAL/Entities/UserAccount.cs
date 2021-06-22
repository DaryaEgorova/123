namespace Maganizer_Project.DAL.Entities
{
    public class UserAccount
    {    
        public ApplicationUser IdentityUser { get; set; }
        public string Password { get; set; }
    }
}
