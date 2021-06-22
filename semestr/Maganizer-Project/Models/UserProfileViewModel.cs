namespace Maganizer_Project.WEB.Models
{
    public class UserProfileViewModel
    {
        public string Username { get; set; }
        public string EmailForContacts { get; set; }
        public string WebSiteUrl { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string About { get; set; }
        public string FullName { get; set; }
        public byte[] Avatar { get; set; }
    }
}
