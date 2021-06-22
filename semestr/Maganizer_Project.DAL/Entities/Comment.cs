using System;

namespace Maganizer_Project.DAL.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
