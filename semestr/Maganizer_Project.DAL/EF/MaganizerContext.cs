using Maganizer_Project.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Maganizer_Project.DAL.EF
{
    public class MaganizerContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        //public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<MessageToAdmin> MessagesToAdmin { get; set; }

        public MaganizerContext(DbContextOptions<MaganizerContext> options)
            : base(options)
        {
        }
    }
}
