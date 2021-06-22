using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maganizer_Project.Models
{
    public class PostEditorPicksViewModel
    {     
        public List<PostEditorPickModel> Posts { get; set; }
    }

    public class PostEditorPickModel
    {
        public string Name { get; set; }
        public byte[] FeaturedImage { get; set; }
        public string AuthorName { get; set; }
        public DateTime PostedOn { get; set; }
    }
}
