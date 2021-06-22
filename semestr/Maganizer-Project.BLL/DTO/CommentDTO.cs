using System;
using System.Collections.Generic;
using System.Text;

namespace Maganizer_Project.BLL.DTO
{
    public class CommentDTO
    {
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public byte[] AuthorAvatar { get; set; }
        public DateTime PostDate { get; set; }
        public int PostId { get; set; }
    }
}
