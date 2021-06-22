using Maganizer_Project.BLL.DTO;
using System.Collections.Generic;

namespace Maganizer_Project.BLL.Interfaces
{
    public interface ITagService
    {
        IEnumerable<TagDTO> GetTags();
        TagArchiveDTO GetTag(string name);
        void DeleteTag(string name);
    }

}
