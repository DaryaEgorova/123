using Maganizer_Project.BLL.DTO;
using System.Collections.Generic;

namespace Maganizer_Project.BLL.Interfaces
{
    public interface IPostService
    {
        void AddPost(EditPostDTO postDTO);
        GetPostDTO GetPostByName(string name);
        IEnumerable<GetPostDTO> GetPosts();

    }
}
