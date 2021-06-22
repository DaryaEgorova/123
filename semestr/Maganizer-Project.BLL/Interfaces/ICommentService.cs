using Maganizer_Project.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maganizer_Project.BLL.Interfaces
{
    public interface ICommentService
    {
        IEnumerable<CommentDTO> GetCommentsOfPost(int postId);
        Task AddComment(CommentDTO commentDTO);
    }
}
