using System;
using System.Collections.Generic;
using System.Text;

namespace Maganizer_Project.BLL.DTO
{
    public class TagArchiveDTO
    {
        public string Name { get; set; }
        public List<PostOfTagDTO> Posts { get; set; }
    }

    public class PostOfTagDTO
    {
        public string PostName { get; set; }
        public string FirstTagName { get; set; }
        public DateTime PostedOn { get; set; }
        public byte[] FeaturedPostImage { get; set; }
        public string AuthorName { get; set; }

    }
}
