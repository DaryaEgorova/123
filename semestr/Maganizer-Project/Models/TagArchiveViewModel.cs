using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maganizer_Project.Models
{
    public class TagArchiveViewModel
    {
        public string TagName { get; set; }
        public List<PostOfTagModel> Posts { get; set; }
    }
    public class PostOfTagModel 
    {
        public string PostName { get; set; }
        public string FirstTagName { get; set; }
        public DateTime PostedOn { get; set; }
        public byte[] FeaturedPostImage { get; set; }
        public string AuthorName { get; set; }
        
    }
}
