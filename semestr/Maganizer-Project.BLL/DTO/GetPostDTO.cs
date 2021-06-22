using System;
using System.Collections.Generic;
using System.Text;

namespace Maganizer_Project.BLL.DTO
{
    public class GetPostDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<TagDTO> Tags { get; set; }
        public DateTime DateOfCreation { get; set; }
        public byte[] FeaturedImage { get; set; }
        public string NextPostName { get; set; }
        public string PreviousPostName { get; set; }
        public string AuthorName { get; set; }
    }
}
