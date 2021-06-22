using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maganizer_Project.DAL.Entities
{
    public class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public List<Post> Posts { get; set; }
    }
}
