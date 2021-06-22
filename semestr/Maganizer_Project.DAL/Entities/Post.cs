using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maganizer_Project.DAL.Entities
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        //public Category Category { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreation { get; set; }
        public byte[] FeaturedImage { get; set; }
        public List<Tag> Tags { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
