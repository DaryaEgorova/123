using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maganizer_Project.Models
{
    public class CommentViewModel
    {
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public byte[] AuthorAvatar { get; set; }
        public string PostDate { get; set; }
    }
}
